using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrollmentDateConfirmedIntegrationEvent : IntegrationEventBase
    {
        public EnrollmentDateConfirmedIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }
        
        public Guid ResponseId { get; }
    }
}