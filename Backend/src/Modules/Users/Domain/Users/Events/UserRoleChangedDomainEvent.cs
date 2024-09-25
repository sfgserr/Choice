using BuildingBlocks.Domain;

namespace Users.Domain.Users.Events
{
    public class UserRoleChangedDomainEvent : DomainEventBase
    {
        public UserRoleChangedDomainEvent(UserId userId, UserRole iconUri)
        {
            UserId = userId;
            UserRole = iconUri;
        }

        public UserId UserId { get; }

        public UserRole UserRole { get; }
    }
}
