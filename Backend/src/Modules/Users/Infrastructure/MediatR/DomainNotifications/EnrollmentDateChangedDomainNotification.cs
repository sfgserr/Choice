using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrollmentDateChangedDomainNotification : DomainNotificationBase<EnrollmentDateChangedDomainEvent>
    {
        public EnrollmentDateChangedDomainNotification(EnrollmentDateChangedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}