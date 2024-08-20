using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Events
{
    public class OrderResponseCreatedDomainEvent : DomainEventBase
    {
        public OrderResponseCreatedDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
