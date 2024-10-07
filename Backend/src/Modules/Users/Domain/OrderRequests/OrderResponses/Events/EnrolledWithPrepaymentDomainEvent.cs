using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrolledWithPrepaymentDomainEvent : DomainEventBase
    {
        public EnrolledWithPrepaymentDomainEvent(OrderResponseId responseId, double cost)
        {
            ResponseId = responseId;
            Cost = cost;
        }

        public OrderResponseId ResponseId { get; }

        public double Cost { get; }
    }
}
