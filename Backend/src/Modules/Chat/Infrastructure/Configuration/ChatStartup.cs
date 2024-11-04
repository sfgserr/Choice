using Autofac;
using BuildingBlocks.Application.Authentication;
using BuildingBlocks.Application.Events;
using Chat.Infrastructure.Configuration.Authentication;
using Chat.Infrastructure.Configuration.Data;
using Chat.Infrastructure.Configuration.Events;
using Chat.Infrastructure.Configuration.Logging;
using Chat.Infrastructure.Configuration.Outbox;
using Chat.Infrastructure.Configuration.Processing;
using Chat.Infrastructure.Configuration.Quartz;
using Chat.Infrastructure.Configuration.SignalR;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Chat.Infrastructure.Configuration
{
    public static class ChatStartup<T> where T : Hub
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString, 
            ILogger logger, 
            IUserService userService,
            IBus bus,
            IHubContext<T> hubContext)
        {
            ConfigureCompositionRoot(connectionString, logger, userService, bus, hubContext);

            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString, 
            ILogger logger, 
            IUserService userService,
            IBus bus,
            IHubContext<T> hubContext)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new AuthenticationModule(userService));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new EventBusModule(bus));
            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Chat")));
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new SignalRModule<T>(hubContext));
            
            _container = containerBuilder.Build();

            ChatCompositionRoot.SetContainer(_container);
        }
    }
}
