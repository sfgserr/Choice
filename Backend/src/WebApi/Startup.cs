using Autofac.Extensions.DependencyInjection;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ILogger = Serilog.ILogger;
using Serilog;
using Users.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hellang.Middleware.ProblemDetails;
using WebApi.Configuration.Authorization;
using WebApi.Configuration.Validation;
using Autofac;
using BuildingBlocks.Application.Authentication;
using WebApi.Configuration.Authentication;
using WebApi.Modules.Users;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Events;
using Identity.Infrastructure.Configuration;
using Identity.Infrastructure.Configuration.EventBus;
using MassTransit;
using WebApi.Configuration.EventBus;
using WebApi.Modules.Identity;
using Payments.Infrastructure.Configuration;

namespace WebApi
{
    public class Startup
    {
        private ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigureLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            string issuer = Configuration["JwtSettings:Issuer"]!;
            string audience = Configuration["JwtSettings:Audience"]!;
            string secretKey = Configuration["JwtSettings:SecretKey"]!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                    o.IncludeErrorDetails = true;
                });

            services.AddSignalR();
            services.AddAuthorization();

            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationProblemDetails(ex));
            });
            
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<JwtProvider>(x => new(new(issuer, audience, secretKey)));
            services.AddSingleton<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, HasPermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IClaimsTransformation, CustomClaimsTransformation>();
            services.AddSingleton<IUserService, UserService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new UsersAutofacModule());
            builder.RegisterModule(new IdentityAutofacModule());
            builder.RegisterModule(new EventBusModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            var connectionString = Configuration["PostgreSqlSettings:ConnectionString"]!;
            var userService = container.Resolve<IUserService>();
            var eventBus = container.Resolve<IEventBus>();
            var bus = container.Resolve<IBusControl>();
            
            UsersStartup.Initialize(
                connectionString, 
                _logger, 
                userService,
                eventBus);
            
            IdentityStartup.Initialize(
                connectionString,
                _logger,
                userService,
                eventBus);

            PaymentsStartup.Initialize(
                connectionString,
                _logger,
                userService,
                eventBus);

            bus.StartAsync().GetAwaiter().GetResult();
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseProblemDetails();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: 
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
