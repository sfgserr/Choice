using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class OrderStatusChangedIntegrationEvent : IIntegrationEvent
    {
        public OrderStatusChangedIntegrationEvent(Guid responseId, string status)
        {
            ResponseId = responseId;
            Status = status;
        }

        public Guid ResponseId { get; }
        
        public string Status { get; }
    }
}