using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class EnrollmentDateChangedDomainEvent : DomainEventBase
    {
        public EnrollmentDateChangedDomainEvent(
            OrderResponseId responseId,
            DateTime? previousEnrollmentDate,
            UserId userChangedEnrollmentDateId, 
            bool isEnrollmentDateConfirmed)
        {
            ResponseId = responseId;
            PreviousEnrollmentDate = previousEnrollmentDate;
            UserChangedEnrollmentDateId = userChangedEnrollmentDateId;
            IsEnrollmentDateConfirmed = isEnrollmentDateConfirmed;
        }

        public OrderResponseId ResponseId { get; }

        public DateTime? PreviousEnrollmentDate { get; }

        public UserId UserChangedEnrollmentDateId { get; }

        public bool IsEnrollmentDateConfirmed { get; }
    }
}
