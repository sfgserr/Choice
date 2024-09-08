using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderPaidDomainEvent : DomainEventBase
    {
        public OrderPaidDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
