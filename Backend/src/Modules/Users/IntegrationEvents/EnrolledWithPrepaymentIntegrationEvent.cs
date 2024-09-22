using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledWithIntegrationEvent : IntegrationEventBase
    {
        public EnrolledWithIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }
        
        public Guid ResponseId { get; }
    }
}