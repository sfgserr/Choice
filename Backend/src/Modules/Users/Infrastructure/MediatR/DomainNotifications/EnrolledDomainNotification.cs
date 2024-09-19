using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrolledDomainNotification : DomainNotificationBase<EnrolledDomainEvent>
    {
        public EnrolledDomainNotification(EnrolledDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}