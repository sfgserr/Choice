using Autofac;
using BuildingBlocks.Application.Authentication;
using MassTransit;
using Serilog;
using Users.Infrastructure.Configuration.Authentication;
using Users.Infrastructure.Configuration.Data;
using Users.Infrastructure.Configuration.DomainEventsDispatching;
using Users.Infrastructure.Configuration.Events;
using Users.Infrastructure.Configuration.GeoCoding;
using Users.Infrastructure.Configuration.Logging;
using Users.Infrastructure.Configuration.Mediation;
using Users.Infrastructure.Configuration.Outbox;
using Users.Infrastructure.Configuration.Processing;
using Users.Infrastructure.Configuration.Quartz;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.Configuration
{
    public static class UsersStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString, 
            ILogger logger, 
            IUserService userService,
            IBus bus)
        {
            var usersLogger = logger.ForContext("Module", "Users");
            
            ConfigureCompositionRoot(connectionString, usersLogger, userService, bus);

            QuartzStartup.Initialize(usersLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString, 
            ILogger logger, 
            IUserService userService,
            IBus bus)
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
            containerBuilder.RegisterModule(new EventBusModule(bus));
            containerBuilder.RegisterModule(new GeoCodingModule());
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());

            _container = containerBuilder.Build();

            UsersCompositionRoot.SetContainer(_container);
        }
    }
}
