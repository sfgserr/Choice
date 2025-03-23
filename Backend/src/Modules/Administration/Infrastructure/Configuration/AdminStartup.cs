using Administration.Infrastructure.Configuration.Data;
using Administration.Infrastructure.Configuration.DomainEventsDispatching;
using Administration.Infrastructure.Configuration.Events;
using Administration.Infrastructure.Configuration.GeoCoding;
using Administration.Infrastructure.Configuration.Logging;
using Administration.Infrastructure.Configuration.Outbox;
using Administration.Infrastructure.Configuration.Processing;
using Autofac;
using MassTransit;
using Serilog;

namespace Administration.Infrastructure.Configuration
{
    public static class AdminStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString, 
            ILogger logger, 
            IBus bus,
            IHttpClientFactory factory,
            string apiKey)
        {
            ConfigureCompositionRoot(connectionString, logger, bus, factory, apiKey);
        }

        private static void ConfigureCompositionRoot(
            string connectionString, 
            ILogger logger, 
            IBus bus,
            IHttpClientFactory factory,
            string apiKey)
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new DomainEventsDispatchingModule([]));
            containerBuilder.RegisterModule(new EventBusModule(bus));
            containerBuilder.RegisterModule(new GeoCodingModule(factory, apiKey));
            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Administration")));
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new ProcessingModule());

            _container = containerBuilder.Build();

            AdminCompositionRoot.SetContainer(_container);
        }
    }
}
