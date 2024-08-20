using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Events
{
    public class EnrolledDomainEvent : DomainEventBase
    {
        public EnrolledDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
