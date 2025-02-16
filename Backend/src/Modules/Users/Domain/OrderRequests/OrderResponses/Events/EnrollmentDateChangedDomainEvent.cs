using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrollmentDateChangedDomainEvent : DomainEventBase
    {
        public EnrollmentDateChangedDomainEvent(
            OrderResponseId responseId,
            DateTime previousEnrollmentDate,
            UserId toUserId)
        {
            ResponseId = responseId;
            PreviousEnrollmentDate = previousEnrollmentDate;
            ToUserId = toUserId;
        }

        public OrderResponseId ResponseId { get; }

        public DateTime PreviousEnrollmentDate { get; }

        public UserId ToUserId { get; }
    }
}
