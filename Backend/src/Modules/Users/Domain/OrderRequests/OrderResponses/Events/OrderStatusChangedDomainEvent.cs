using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderStatusChangedDomainEvent : DomainEventBase
    {
        public OrderStatusChangedDomainEvent(
            OrderRequestId requestId, 
            OrderResponseId responseId, 
            OrderStatus status, 
            UserId toUserId)
        {
            RequestId = requestId;
            ResponseId = responseId;
            Status = status;
            ToUserId = toUserId;
        }

        public OrderRequestId RequestId { get; }
        
        public OrderResponseId ResponseId { get; }

        public OrderStatus Status { get; }
        
        public UserId ToUserId { get; }
    }
}
