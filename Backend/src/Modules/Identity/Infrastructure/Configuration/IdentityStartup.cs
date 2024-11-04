using Autofac;
using BuildingBlocks.Application.Authentication;
using BuildingBlocks.Application.Events;
using Identity.Infrastructure.Configuration.Authentication;
using Identity.Infrastructure.Configuration.Data;
using Identity.Infrastructure.Configuration.DomainEventsDispatching;
using Identity.Infrastructure.Configuration.Events;
using Identity.Infrastructure.Configuration.Logging;
using Identity.Infrastructure.Configuration.Outbox;
using Identity.Infrastructure.Configuration.Processing;
using Identity.Infrastructure.Configuration.Quartz;
using MassTransit;
using Serilog;

namespace Identity.Infrastructure.Configuration
{
    public static class IdentityStartup
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
            IBus eventBus)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AuthenticationModule(userService));
            builder.RegisterModule(new DataAccessModule(connectionString));
            builder.RegisterModule(new DomainEventsDispatchingModule([]));
            builder.RegisterModule(new EventBusModule(eventBus));
            builder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Identity")));
            builder.RegisterModule(new OutboxModule());
            builder.RegisterModule(new ProcessingModule());
            builder.RegisterModule(new QuartzModule());
            
            _container = builder.Build();
            IdentityCompositionRoot.SetContainer(_container);
        }
    }
}