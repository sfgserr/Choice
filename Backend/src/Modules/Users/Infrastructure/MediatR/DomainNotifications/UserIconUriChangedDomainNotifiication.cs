using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.Users.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    public class UserIconUriChangedDomainNotification : DomainNotificationBase<UserIconUriChangedDomainEvent>
    {
        public UserIconUriChangedDomainNotification(UserIconUriChangedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}