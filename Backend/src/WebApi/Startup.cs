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
using Administration.Infrastructure.Configuration;
using Hellang.Middleware.ProblemDetails;
using WebApi.Configuration.Authorization;
using WebApi.Configuration.Validation;
using Autofac;
using BuildingBlocks.Application.Authentication;
using WebApi.Configuration.Authentication;
using WebApi.Modules.Users;
using Identity.Infrastructure.Configuration;
using Chat.Infrastructure.Configuration;
using Identity.Infrastructure.Configuration.Identity;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using WebApi.Configuration.EventBus;
using WebApi.Modules.Identity;
using Payments.Infrastructure.Configuration;
using WebApi.Configuration.Chat;
using WebApi.Modules.Admin;
using WebApi.Modules.Chat;
using WebApi.Modules.Payments;

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

            services.AddHttpsRedirection(o => o.HttpsPort = 6473);
            
            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationProblemDetails(ex));
            });

            var jwtOptions = new JwtOptions(issuer, audience, secretKey);
            
            services.AddIdentity(jwtOptions);
            
            services.AddSingleton<JwtProvider>(x => new(jwtOptions));
            services.AddSingleton<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, HasPermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IClaimsTransformation, CustomClaimsTransformation>();
            services.AddSingleton<IUserService, UserService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new UsersAutofacModule());
            builder.RegisterModule(new PaymentsAutofacModule());
            builder.RegisterModule(new IdentityAutofacModule());
            builder.RegisterModule(new ChatAutofacModule());
            builder.RegisterModule(new AdminAutofacModule());
            builder.RegisterModule(new EventBusModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            var connectionString = Configuration["PostgreSqlSettings:ConnectionString"]!;
            var userService = container.Resolve<IUserService>();
            var bus = container.Resolve<IBusControl>();
            var hub = container.Resolve<IHubContext<ChatHub>>();
            
            UsersStartup.Initialize(
                connectionString, 
                _logger, 
                userService,
                bus);
            
            IdentityStartup.Initialize(
                connectionString,
                _logger,
                userService,
                bus);

            PaymentsStartup.Initialize(
                connectionString,
                _logger,
                userService,
                bus);
            
            ChatStartup<ChatHub>.Initialize(
                connectionString,
                _logger,
                userService,
                bus,
                hub);

            AdminStartup.Initialize(
                connectionString,
                _logger,
                bus);
            
            bus.StartAsync().GetAwaiter().GetResult();
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseProblemDetails();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<ChatHub>("chat");
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
