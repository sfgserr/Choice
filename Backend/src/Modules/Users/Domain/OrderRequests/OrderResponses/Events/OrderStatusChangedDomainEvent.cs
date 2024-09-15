using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderStatusChangedDomainEvent : DomainEventBase
    {
        public OrderStatusChangedDomainEvent(OrderRequestId requestId, OrderResponseId responseId, OrderStatus status)
        {
            RequestId = requestId;
            ResponseId = responseId;
            Status = status;
        }

        public OrderRequestId RequestId { get; }
        
        public OrderResponseId ResponseId { get; }

        public OrderStatus Status { get; }
    }
}
