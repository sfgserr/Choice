using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
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
