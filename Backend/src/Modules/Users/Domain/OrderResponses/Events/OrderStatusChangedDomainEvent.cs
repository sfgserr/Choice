using BuildingBlocks.Domain;
using Users.Domain.OrderRequests;

namespace Users.Domain.OrderResponses.Events
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
