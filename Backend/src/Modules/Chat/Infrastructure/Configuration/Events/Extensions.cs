using Autofac;
using BuildingBlocks.Application.Events;
using MassTransit;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Configuration.Events
{
    public static class Extensions
    {
        public static void AddChatConsumers(this IReceiveEndpointConfigurator cfg)
        {
            cfg
                .AddHandler<EnrolledIntegrationEvent>()
                .AddHandler<EnrollmentDateChangedIntegrationEvent>()
                .AddHandler<EnrollmentDateConfirmedIntegrationEvent>()
                .AddHandler<OrderResponseCreatedIntegrationEvent>()
                .AddHandler<OrderStatusChangedIntegrationEvent>()
                .AddHandler<UserCreatedIntegrationEvent>()
                .AddHandler<UserDataChangedIntegrationEvent>()
                .AddHandler<UserIconUriChangedIntegrationEvent>();
        }

        private static IReceiveEndpointConfigurator AddHandler<T>(this IReceiveEndpointConfigurator cfg)
            where T : IntegrationEventBase
        {
            cfg.Handler<T>(async c =>
            {
                using var scope = ChatCompositionRoot.BeginLifetimeScope();

                var type = typeof(IBusConsumer<>).MakeGenericType(typeof(T));

                var consumer = scope.Resolve(type) as IBusConsumer<T> ??
                               throw new ApplicationException($"No Bus Consumer registered for {typeof(T).Name}");

                await consumer.Consume(c.Message);
            });

            return cfg;
        }
    }
}