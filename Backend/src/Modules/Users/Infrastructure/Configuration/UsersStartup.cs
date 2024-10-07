using Autofac;
using BuildingBlocks.Application.Authentication;
using BuildingBlocks.Application.Events;
using Serilog;
using Users.Infrastructure.Configuration.Authentication;
using Users.Infrastructure.Configuration.Data;
using Users.Infrastructure.Configuration.DomainEventsDispatching;
using Users.Infrastructure.Configuration.EventBus;
using Users.Infrastructure.Configuration.GeoCoding;
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

        public static void Initialize(
            string connectionString, 
            ILogger logger, 
            IUserService userService,
            IEventBus eventBus)
        {
            ConfigureCompositionRoot(connectionString, logger, userService, eventBus);

            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString, 
            ILogger logger, 
            IUserService userService,
            IEventBus eventBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new AuthenticationModule(userService));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));

            var mappings = new Dictionary<string, Type>()
            {
                [nameof(EnrolledDomainNotification)] = typeof(EnrolledDomainNotification),
                [nameof(EnrolledWithPrepaymentDomainNotification)] = typeof(EnrolledWithPrepaymentDomainNotification),
                [nameof(EnrollmentDateChangedDomainNotification)] = typeof(EnrollmentDateChangedDomainNotification),
                [nameof(EnrollmentDateConfirmedDomainNotification)] = typeof(EnrollmentDateConfirmedDomainNotification),
                [nameof(OrderPaidDomainNotification)] = typeof(OrderPaidDomainNotification),
                [nameof(OrderResponseCreatedDomainNotification)] = typeof(OrderResponseCreatedDomainNotification),
                [nameof(OrderStatusChangedDomainNotification)] = typeof(OrderStatusChangedDomainNotification),
                [nameof(ReviewCreatedDomainNotification)] = typeof(ReviewCreatedDomainNotification),
                [nameof(UserCreatedDomainNotification)] = typeof(UserCreatedDomainNotification),
                [nameof(UserDataChangedDomainNotification)] = typeof(UserDataChangedDomainNotification),
                [nameof(UserRoleChangedDomainNotification)] = typeof(UserRoleChangedDomainNotification),
            };

            containerBuilder.RegisterModule(new DomainEventsDispatchingModule(mappings));
            containerBuilder.RegisterModule(new EventBusModule(eventBus));
            containerBuilder.RegisterModule(new GeoCodingModule());
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
