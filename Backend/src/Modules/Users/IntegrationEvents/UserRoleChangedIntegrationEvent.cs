using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class UserRoleChangedIntegrationEvent : IntegrationEventBase
    {
        public UserRoleChangedIntegrationEvent(Guid id, Guid userId, string userRole) : base(id)
        {
            UserId = userId;
            UserRole = userRole;
        }

        public Guid UserId { get; }

        public string UserRole { get; }  
    }
}
