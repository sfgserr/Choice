using BuildingBlocks.Domain;
using Users.Domain.Users.Clients;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrolledWithPrepaymentDomainEvent : DomainEventBase
    {
        public EnrolledWithPrepaymentDomainEvent(
            OrderResponseId responseId, 
            ClientId clientId, 
            double cost)
        {
            ResponseId = responseId;
            ClientId = clientId;
            Cost = cost;
        }

        public OrderResponseId ResponseId { get; }
        
        public ClientId ClientId { get; }

        public double Cost { get; }
    }
}
