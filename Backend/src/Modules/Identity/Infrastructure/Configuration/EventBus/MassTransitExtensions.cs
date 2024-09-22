using Autofac;
using BuildingBlocks.Application.Events;
using MassTransit;
using Users.IntegrationEvents;

namespace Identity.Infrastructure.Configuration.EventBus
{
    public static class MassTransitExtensions
    {
        public static void AddIdentityConsumers(this IReceiveEndpointConfigurator cfg)
        {
            cfg.Handler<UserCreatedIntegrationEvent>(async c =>
            {
                using var scope = IdentityCompositionRoot.BeginLifetimeScope();
                
                var consumer = scope.Resolve<IBusConsumer<UserCreatedIntegrationEvent>>();

                await consumer.Consume(c.Message);
            });
        }
    }
}