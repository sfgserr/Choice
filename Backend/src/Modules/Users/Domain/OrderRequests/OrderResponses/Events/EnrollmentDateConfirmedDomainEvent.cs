using BuildingBlocks.Domain;
using Users.Domain.Users.Clients;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrollmentDateConfirmedDomainEvent : DomainEventBase
    {
        public EnrollmentDateConfirmedDomainEvent(OrderResponseId responseId, ClientId toUserId)
        {
            ResponseId = responseId;
            ToUserId = toUserId;
        }

        public OrderResponseId ResponseId { get; }
        
        public ClientId ToUserId { get; }
    }
}
