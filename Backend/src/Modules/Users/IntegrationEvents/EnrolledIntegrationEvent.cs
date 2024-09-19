using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledIntegrationEvent : IntegrationEventBase
    {
        public EnrolledIntegrationEvent(Guid responseId) : base(Guid.NewGuid())
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
