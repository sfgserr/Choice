using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.Users.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class UserDataChangedDomainNotification : DomainNotificationBase<UserDataChangedDomainEvent>
    {
        public UserDataChangedDomainNotification(UserDataChangedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
