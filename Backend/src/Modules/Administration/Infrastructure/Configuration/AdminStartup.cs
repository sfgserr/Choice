using Administration.Infrastructure.Configuration.Data;
using Administration.Infrastructure.Configuration.Events;
using Administration.Infrastructure.Configuration.Logging;
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
            IBus bus)
        {
            ConfigureCompositionRoot(connectionString, logger, bus);
        }

        private static void ConfigureCompositionRoot(
            string connectionString, 
            ILogger logger, 
            IBus bus)
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new EventBusModule(bus));
            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Users")));
            containerBuilder.RegisterModule(new ProcessingModule());

            _container = containerBuilder.Build();

            AdminCompositionRoot.SetContainer(_container);
        }
    }
}
