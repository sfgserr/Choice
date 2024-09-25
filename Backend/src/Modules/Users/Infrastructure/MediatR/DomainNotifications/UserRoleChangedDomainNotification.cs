using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.Users.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class UserRoleChangedDomainNotification : DomainNotificationBase<UserRoleChangedDomainEvent>
    {
        public UserRoleChangedDomainNotification(UserRoleChangedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
