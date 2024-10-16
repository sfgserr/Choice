using BuildingBlocks.Domain;

namespace Users.Domain.Users.Events
{
    public class UserRoleChangedDomainEvent : DomainEventBase
    {
        public UserRoleChangedDomainEvent(UserId userId, string userRole)
        {
            UserId = userId;
            UserRole = userRole;
        }

        public UserId UserId { get; }

        public string UserRole { get; }
    }
}
