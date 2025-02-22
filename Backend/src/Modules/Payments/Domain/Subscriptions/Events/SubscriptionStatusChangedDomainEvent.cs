using BuildingBlocks.Domain;

namespace Payments.Domain.Subscriptions.Events
{
    public class SubscriptionStatusChangedDomainEvent : DomainEventBase
    {
        public SubscriptionStatusChangedDomainEvent(SubscriberId subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public SubscriberId SubscriberId { get; }
    }
}