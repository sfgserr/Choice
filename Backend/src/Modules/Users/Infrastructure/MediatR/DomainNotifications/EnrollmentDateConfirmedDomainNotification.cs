using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrollmentDateConfirmedDomainNotification : 
        DomainNotificationBase<EnrollmentDateConfirmedDomainEvent>
    {
        public EnrollmentDateConfirmedDomainNotification(EnrollmentDateConfirmedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}