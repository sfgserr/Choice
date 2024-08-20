using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Events
{
    public class EnrollmentDateConfirmedDomainEvent : DomainEventBase
    {
        public EnrollmentDateConfirmedDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
