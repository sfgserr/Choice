using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrollmentDateChangedIntegrationEvent : IntegrationEventBase
    {
        public EnrollmentDateChangedIntegrationEvent(
            Guid id,
            Guid responseId,
            DateTime previousEnrollmentDate,
            Guid userChangedEnrollmentDateId,
            bool isEnrollmentDateConfirmed) : base(id)
        {
            ResponseId = responseId;
            PreviousEnrollmentDate = previousEnrollmentDate;
            UserChangedEnrollmentDateId = userChangedEnrollmentDateId;
            IsEnrollmentDateConfirmed = isEnrollmentDateConfirmed;
        }

        public Guid ResponseId { get; }

        public DateTime PreviousEnrollmentDate { get; }

        public Guid UserChangedEnrollmentDateId { get; }

        public bool IsEnrollmentDateConfirmed { get; }
    }
}