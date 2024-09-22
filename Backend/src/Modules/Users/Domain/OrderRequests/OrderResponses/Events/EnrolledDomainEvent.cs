using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrolledDomainEvent : DomainEventBase
    {
        public EnrolledDomainEvent(OrderResponseId responseId, OrderRequestId requestId)
        {
            ResponseId = responseId;
            RequestId = requestId;
        }

        public OrderResponseId ResponseId { get; }
        
        public OrderRequestId RequestId { get; }
    }
}
