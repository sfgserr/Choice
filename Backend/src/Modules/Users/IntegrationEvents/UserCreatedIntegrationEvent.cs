using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IIntegrationEvent
    {
        public UserCreatedIntegrationEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
