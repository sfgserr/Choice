using Autofac.Extensions.DependencyInjection;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ILogger = Serilog.ILogger;
using Serilog;
using Users.Infrastructure.Configuration;
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
using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Configuration.Data;
using Identity.Infrastructure.Configuration.Identity;
using Identity.Infrastructure.Middlewares.SubscriptionCheck;
using MassTransit;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using OpenIddict.Validation.AspNetCore;
using WebApi.Configuration.EventBus;
using WebApi.Modules.Identity;
using Payments.Infrastructure.Configuration;
using WebApi.Configuration.Authentication.GrantTypeHandling;
using WebApi.Configuration.Authentication.GrantTypeHandling.GrantTypeHandlers;
using WebApi.Configuration.Chat;
using WebApi.Modules.Admin;
using WebApi.Modules.Chat;
using WebApi.Modules.Payments;
using WebApi.Seed;

namespace WebApi
{
    public class Startup
    {
        private ILogger _logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
            
            ConfigureLogger();
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment CurrentEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            string issuer = Configuration["IdentitySettings:Issuer"]!;
            string secretKey = Configuration["IdentitySettings:SecretKey"]!;
            string certificateThumbprint = Configuration["IdentitySettings:Thumbprint"]!;

            services.AddSignalR();
            
            services.AddAuthorization();
            services.AddAuthentication(options => 
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            
            services.AddHttpContextAccessor();
            services.AddHttpClient("Geocode", options =>
            {
                options.BaseAddress = new(Configuration["YandexGeocoder:BaseUrl"]!);
            });
            
            services.AddControllers();
            services.AddSwaggerGen();
            
            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationProblemDetails(ex));
            });

            var identityOptions = new IdentityOptions(issuer, secretKey, certificateThumbprint);
            
            services.AddIdentity(identityOptions, CurrentEnvironment);
            
            services.AddSingleton<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, HasPermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IClaimsTransformation, CustomClaimsTransformation>();
            services.AddSingleton<IUserService, UserService>();
            services.Configure<ClientsOption>(Configuration);
            services.AddSingleton<SeedClients>();
            services.AddSingleton<IGrantTypeHandler, PasswordGrantTypeHandler>();
            services.AddSingleton<IGrantTypeHandler, RefreshTokenGrantTypeHandler>();
            services.AddSingleton<GrantTypeHandlerFactory>();
            services.AddSingleton<IUserIdProvider, SubjectBasedUserIdProvider>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new UsersAutofacModule());
            builder.RegisterModule(new PaymentsAutofacModule());
            builder.RegisterModule(new IdentityAutofacModule());
            builder.RegisterModule(new ChatAutofacModule());
            builder.RegisterModule(new AdminAutofacModule());
            builder.RegisterModule(new EventBusModule());
            builder.RegisterModule(new DataAccessModule(Configuration["PostgreSqlSettings:ConnectionString"]!));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            var connectionString = Configuration["PostgreSqlSettings:ConnectionString"]!;
            var userService = container.Resolve<IUserService>();
            var bus = container.Resolve<IBusControl>();
            var hub = container.Resolve<IHubContext<ChatHub>>();
            var seed = container.Resolve<SeedClients>();
            var clientFactory = container.Resolve<IHttpClientFactory>();
            
            UsersStartup.Initialize(
                connectionString, 
                _logger, 
                userService,
                bus,
                clientFactory,
                Configuration["YandexGeocoder:ApiKey"]!);
            
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
            seed.Seed().GetAwaiter().GetResult();
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseForwardedHeaders();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseSubscriptionCheck();
            
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
