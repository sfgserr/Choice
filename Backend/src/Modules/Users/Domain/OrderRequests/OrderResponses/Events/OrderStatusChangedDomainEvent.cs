using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderStatusChangedDomainEvent : DomainEventBase
    {
        public OrderStatusChangedDomainEvent(OrderRequestId requestId, OrderStatus status)
        {
            RequestId = requestId;
            Status = status;
        }

        public OrderRequestId RequestId { get; }

        public OrderStatus Status { get; }
    }
}
