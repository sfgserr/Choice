using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class OrderStatusChangedIntegrationEvent : IntegrationEventBase
    {
        public OrderStatusChangedIntegrationEvent(Guid id, Guid responseId, string status) : base(id)
        {
            ResponseId = responseId;
            Status = status;
        }

        public Guid ResponseId { get; }
        
        public string Status { get; }
    }
}