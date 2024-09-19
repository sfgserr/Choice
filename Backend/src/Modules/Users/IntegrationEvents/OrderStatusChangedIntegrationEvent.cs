using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class OrderStatusChangedIntegrationEvent : IntegrationEventBase
    {
        public OrderStatusChangedIntegrationEvent(Guid responseId, string status) : base(Guid.NewGuid())
        {
            ResponseId = responseId;
            Status = status;
        }

        public Guid ResponseId { get; }
        
        public string Status { get; }
    }
}