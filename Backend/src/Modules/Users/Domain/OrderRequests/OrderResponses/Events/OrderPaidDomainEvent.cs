using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderPaidDomainEvent : DomainEventBase
    {
        public OrderPaidDomainEvent(OrderResponseId responseId, OrderRequestId requestId)
        {
            ResponseId = responseId;
            RequestId = requestId;
        }

        public OrderResponseId ResponseId { get; }
        
        public OrderRequestId RequestId { get; }
    }
}
