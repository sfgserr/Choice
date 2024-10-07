using Autofac;
using BuildingBlocks.Application.Events;
using MassTransit;
using Payments.IntegrationEvents;

namespace Users.Infrastructure.Configuration.EventBus
{
    internal static class Extensions
    {
        public static void AddIdentityConsumers(this IReceiveEndpointConfigurator cfg)
        {
            cfg.AddHandler<EnrollmentPaidIntegrationEvent>();
        }

        private static IReceiveEndpointConfigurator AddHandler<T>(this IReceiveEndpointConfigurator cfg)
            where T : IntegrationEventBase
        {
            cfg.Handler<T>(async c =>
            {
                using var scope = UsersCompositionRoot.BeginLifetimeScope();

                var type = typeof(IBusConsumer<>).MakeGenericType(typeof(T));

                var consumer = scope.Resolve(type) as IBusConsumer<T> ??
                    throw new ApplicationException($"No Bus Consumer registered for {typeof(T).Name}");

                await consumer.Consume(c.Message);
            });

            return cfg;
        }
    }
}
