using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledWithPrepaymentIntegrationEvent : IntegrationEventBase
    {
        public EnrolledWithPrepaymentIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }
        
        public Guid ResponseId { get; }
    }
}