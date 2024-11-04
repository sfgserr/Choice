using Autofac;
using BuildingBlocks.Application.Events;
using MassTransit;
using Users.IntegrationEvents;

namespace Payments.Infrastructure.Configuration.Events
{
    public static class Extensions
    {
        public static void AddPaymentsConsumers(this IReceiveEndpointConfigurator cfg)
        {
            cfg.AddHandler<EnrolledWithPrepaymentIntegrationEvent>();
        }

        private static IReceiveEndpointConfigurator AddHandler<T>(this IReceiveEndpointConfigurator cfg)
            where T : IntegrationEventBase
        {
            cfg.Handler<T>(async c =>
            {
                using var scope = PaymentsCompositionRoot.BeginLifetimeScope();

                var type = typeof(IBusConsumer<>).MakeGenericType(typeof(T));

                var consumer = scope.Resolve(type) as IBusConsumer<T> ??
                    throw new ApplicationException($"No Bus Consumer registered for {typeof(T).Name}");

                await consumer.Consume(c.Message);
            });

            return cfg;
        }
    }
}
