using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderStatusChangedDomainEvent : DomainEventBase
    {
        public OrderStatusChangedDomainEvent(
            OrderRequestId requestId, 
            OrderResponseId responseId, 
            string status, 
            UserId toUserId)
        {
            RequestId = requestId;
            ResponseId = responseId;
            Status = status;
            ToUserId = toUserId;
        }

        public OrderRequestId RequestId { get; }
        
        public OrderResponseId ResponseId { get; }

        public string Status { get; }
        
        public UserId ToUserId { get; }
    }
}
