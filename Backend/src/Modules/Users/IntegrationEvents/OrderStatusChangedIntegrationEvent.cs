using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class OrderStatusChangedIntegrationEvent : IntegrationEventBase
    {
        public OrderStatusChangedIntegrationEvent(Guid id, Guid responseId, Guid toUserId, string status) : base(id)
        {
            ResponseId = responseId;
            ToUserId = toUserId;
            Status = status;
        }

        public Guid ResponseId { get; }
        
        public Guid ToUserId { get; }
        
        public string Status { get; }
    }
}