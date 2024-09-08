using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrolledWithPrepaymentDomainEvent : DomainEventBase
    {
        public EnrolledWithPrepaymentDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
