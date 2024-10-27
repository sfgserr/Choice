using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class UserIconUriChangedIntegrationEvent : IntegrationEventBase
    {
        public UserIconUriChangedIntegrationEvent(Guid id, Guid userId, string iconUri) : base(id)
        {
            UserId = userId;
            IconUri = iconUri;
        }

        public Guid UserId { get; }
        
        public string IconUri { get; }
    }
}