using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrollmentDateChangedIntegrationEvent : IntegrationEventBase
    {
        public EnrollmentDateChangedIntegrationEvent(
            Guid id,
            Guid responseId,
            DateTime previousEnrollmentDate,
            Guid toUserId) : base(id)
        {
            ResponseId = responseId;
            PreviousEnrollmentDate = previousEnrollmentDate;
            ToUserId = toUserId;
        }

        public Guid ResponseId { get; }

        public DateTime PreviousEnrollmentDate { get; }

        public Guid ToUserId { get; }
    }
}