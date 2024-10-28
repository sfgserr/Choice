using BuildingBlocks.Domain;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class OrderResponseCreatedDomainEvent : DomainEventBase
    {
        public OrderResponseCreatedDomainEvent(OrderResponseId responseId, CompanyId companyId, ClientId clientId)
        {
            ResponseId = responseId;
            CompanyId = companyId;
            ClientId = clientId;
        }

        public OrderResponseId ResponseId { get; }
        
        public CompanyId CompanyId { get; }
        
        public ClientId ClientId { get; }
    }
}
