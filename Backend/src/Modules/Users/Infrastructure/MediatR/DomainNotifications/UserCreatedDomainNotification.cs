using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.Users.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class UserCreatedDomainNotification : DomainNotificationBase<UserCreatedDomainEvent>
    {
        internal UserCreatedDomainNotification(UserCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
