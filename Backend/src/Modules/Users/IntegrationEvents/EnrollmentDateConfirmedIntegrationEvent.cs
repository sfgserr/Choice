using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrollmentDateConfirmedIntegrationEvent : IntegrationEventBase
    {
        public EnrollmentDateConfirmedIntegrationEvent(Guid id, Guid responseId, Guid toUserId) : base(id)
        {
            ResponseId = responseId;
            ToUserId = toUserId;
        }
        
        public Guid ResponseId { get; }
        
        public Guid ToUserId { get; }
    }
}