using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderStatusChangedDomainEvent : DomainEventBase
    {
        public OrderStatusChangedDomainEvent(OrderResponseId responseId, OrderStatus status)
        {
            ResponseId = responseId;
            Status = status;
        }

        public OrderResponseId ResponseId { get; }

        public OrderStatus Status { get; }
    }
}
