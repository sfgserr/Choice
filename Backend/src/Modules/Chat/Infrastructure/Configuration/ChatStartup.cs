using Autofac;
using BuildingBlocks.Application.Authentication;
using Chat.Infrastructure.Configuration.Authentication;
using Chat.Infrastructure.Configuration.Data;
using Chat.Infrastructure.Configuration.DomainEventsDispatching;
using Chat.Infrastructure.Configuration.Events;
using Chat.Infrastructure.Configuration.Logging;
using Chat.Infrastructure.Configuration.Mediation;
using Chat.Infrastructure.Configuration.Outbox;
using Chat.Infrastructure.Configuration.Processing;
using Chat.Infrastructure.Configuration.Quartz;
using Chat.Infrastructure.Configuration.SignalR;
using Chat.Infrastructure.Configuration.Users;
using Chat.Infrastructure.MediatR.DomainNotifications;
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
            var chatLogger = logger.ForContext("Module", "Chat");
            
            ConfigureCompositionRoot(connectionString, chatLogger, userService, bus, hubContext);

            QuartzStartup.Initialize(chatLogger);
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

            var mappings = new Dictionary<string, Type>
            {
                [nameof(MessageCreatedDomainNotification)] = typeof(MessageCreatedDomainNotification)
            };
            
            containerBuilder.RegisterModule(new DomainEventsDispatchingModule(mappings));
            containerBuilder.RegisterModule(new EventBusModule(bus));
            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Chat")));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new SignalRModule<T>(hubContext));
            containerBuilder.RegisterModule(new UsersAutofacModule());
            
            _container = containerBuilder.Build();

            ChatCompositionRoot.SetContainer(_container);
        }
    }
}
