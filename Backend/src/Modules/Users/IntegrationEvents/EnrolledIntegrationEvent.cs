using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledIntegrationEvent : IntegrationEventBase
    {
        public EnrolledIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
