using BuildingBlocks.Application.Events;
using Identity.Application.Users.ToggleSubscription;
using Identity.Infrastructure.Data.InternalCommands;
using Payments.IntegrationEvents;

namespace Identity.Infrastructure.Consumers
{
    internal class SubscriptionStatusChangedConsumer : IBusConsumer<SubscriptionStatusChangedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal SubscriptionStatusChangedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(SubscriptionStatusChangedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new ToggleSubscriptionCommand(@event.Id, @event.SubscriberId));
        }
    }
}