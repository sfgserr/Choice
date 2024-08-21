using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderResponses.Events
{
    public class EnrollmentDateChangedDomainEvent : DomainEventBase
    {
        public EnrollmentDateChangedDomainEvent(
            OrderResponseId responseId,
            DateTime? oldEnrollmentDate,
            DateTime? enrollmentDate, 
            UserId userChangedEnrollmentDateId, 
            bool isEnrollmentDateConfirmed)
        {
            ResponseId = responseId;
            EnrollmentDate = enrollmentDate;
            UserChangedEnrollmentDateId = userChangedEnrollmentDateId;
            IsEnrollmentDateConfirmed = isEnrollmentDateConfirmed;
        }

        public OrderResponseId ResponseId { get; }

        public DateTime? OldEnrollmentDate { get; }

        public DateTime? EnrollmentDate { get; }

        public UserId UserChangedEnrollmentDateId { get; }

        public bool IsEnrollmentDateConfirmed { get; }
    }
}
