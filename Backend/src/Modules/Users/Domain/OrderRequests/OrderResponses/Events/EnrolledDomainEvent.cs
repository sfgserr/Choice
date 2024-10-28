using BuildingBlocks.Domain;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrolledDomainEvent : DomainEventBase
    {
        public EnrolledDomainEvent(OrderResponseId responseId, OrderRequestId requestId, CompanyId companyId)
        {
            ResponseId = responseId;
            RequestId = requestId;
            CompanyId = companyId;
        }

        public OrderResponseId ResponseId { get; }
        
        public OrderRequestId RequestId { get; }
        
        public CompanyId CompanyId { get; }
    }
}
