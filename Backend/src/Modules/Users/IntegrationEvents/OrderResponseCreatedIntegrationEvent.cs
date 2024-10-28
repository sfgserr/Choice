using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class OrderResponseCreatedIntegrationEvent : IntegrationEventBase
    {
        public OrderResponseCreatedIntegrationEvent(
            Guid id, 
            Guid responseId, 
            Guid fromUserId, 
            Guid toUserId) : base(id)
        {
            ResponseId = responseId;
            FromUserId = fromUserId;
            ToUserId = toUserId;
        }

        public Guid ResponseId { get; }
        
        public Guid FromUserId { get; }
        
        public Guid ToUserId { get; }
    }
}
