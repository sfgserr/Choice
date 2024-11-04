using Autofac;
using BuildingBlocks.Application.Authentication;
using BuildingBlocks.Application.Events;
using MassTransit;
using Payments.Infrastructure.Configuration.Authentication;
using Payments.Infrastructure.Configuration.Data;
using Payments.Infrastructure.Configuration.DomainEventsDispatching;
using Payments.Infrastructure.Configuration.Events;
using Payments.Infrastructure.Configuration.Logging;
using Payments.Infrastructure.Configuration.Mediation;
using Payments.Infrastructure.Configuration.Outbox;
using Payments.Infrastructure.Configuration.Processing;
using Payments.Infrastructure.Configuration.Quartz;
using Payments.Infrastructure.MediatR.DomainNotifications;
using Serilog;

namespace Payments.Infrastructure.Configuration
{
    public static class PaymentsStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            ILogger logger,
            IUserService userService,
            IBus bus)
        {
            ConfigureCompositionRoot(connectionString, logger, userService, bus);

            QuartzStartup.Initialize();
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
                [nameof(EnrollmentPaidDomainNotification)] = typeof(EnrollmentPaidDomainNotification)
            };

            containerBuilder.RegisterModule(new DomainEventsDispatchingModule(mappings));
            containerBuilder.RegisterModule(new EventBusModule(bus));
            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Payments")));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());

            _container = containerBuilder.Build();

            PaymentsCompositionRoot.SetContainer(_container);
        }
    }
}
