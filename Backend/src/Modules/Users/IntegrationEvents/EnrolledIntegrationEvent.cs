using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledIntegrationEvent : IIntegrationEvent
    {
        public EnrolledIntegrationEvent(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
