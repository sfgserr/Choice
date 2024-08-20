using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Events
{
    public class OrderPaidDomainEvent : DomainEventBase
    {
        internal OrderPaidDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
