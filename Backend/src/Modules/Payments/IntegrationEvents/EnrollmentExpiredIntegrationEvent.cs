using BuildingBlocks.Application.Events;

namespace Payments.IntegrationEvents
{
    public class EnrollmentExpiredIntegrationEvent : IntegrationEventBase
    {
        public EnrollmentExpiredIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }
        
        public Guid ResponseId { get; }
    }
}