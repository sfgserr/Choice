using Autofac;
using Serilog;
using Users.Infrastructure.Configuration.Authentication;
using Users.Infrastructure.Configuration.Data;
using Users.Infrastructure.Configuration.DomainEventsDispatching;
using Users.Infrastructure.Configuration.Logging;
using Users.Infrastructure.Configuration.Mediation;
using Users.Infrastructure.Configuration.Outbox;
using Users.Infrastructure.Configuration.Processing;
using Users.Infrastructure.Configuration.Quartz;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.Configuration
{
    public class UsersStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString, ILogger logger)
        {
            ConfigureCompositionRoot(connectionString, logger);

            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(string connectionString, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new AuthenticationModule());
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));

            var mappings = new Dictionary<string, Type>()
            {
                [nameof(UserCreatedDomainNotification)] = typeof(UserCreatedDomainNotification)
            };

            containerBuilder.RegisterModule(new DomainEventsDispatchingModule(mappings));
            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Users")));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());

            _container = containerBuilder.Build();

            UsersCompositionRoot.SetContainer(_container);
        }
    }
}
