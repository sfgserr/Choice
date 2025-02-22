using BuildingBlocks.Application.Events;

namespace Payments.IntegrationEvents
{
    public class SubscriptionStatusChangedIntegrationEvent : IntegrationEventBase
    {
        public SubscriptionStatusChangedIntegrationEvent(Guid id, Guid subscriberId) : base(id)
        {
            SubscriberId = subscriberId;
        }
        
        public Guid SubscriberId { get; }
    }
}