using BuildingBlocks.Application.Events;

namespace Payments.IntegrationEvents
{
    public class EnrollmentPaidIntegrationEvent : IntegrationEventBase
    {
        public EnrollmentPaidIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
