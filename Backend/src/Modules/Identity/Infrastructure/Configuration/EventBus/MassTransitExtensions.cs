using Autofac;
using BuildingBlocks.Application.Events;
using MassTransit;
using Users.IntegrationEvents;

namespace Identity.Infrastructure.Configuration.EventBus
{
    public static class MassTransitExtensions
    {
        public static void AddIdentityConsumers(this IReceiveEndpointConfigurator cfg, IComponentContext context)
        {
            cfg.Handler<UserCreatedIntegrationEvent>(async c =>
            {
                var consumer = context.Resolve<IBusConsumer<UserCreatedIntegrationEvent>>();

                await consumer.Consume(c.Message);
            });
        }
    }
}