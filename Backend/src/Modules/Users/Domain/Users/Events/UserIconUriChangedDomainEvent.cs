using BuildingBlocks.Domain;

namespace Users.Domain.Users.Events
{
    public class UserIconUriChangedDomainEvent : DomainEventBase
    {
        public UserIconUriChangedDomainEvent(UserId userId, string iconUri)
        {
            UserId = userId;
            IconUri = iconUri;
        }

        public UserId UserId { get; }
        
        public string IconUri { get; }
    }
}