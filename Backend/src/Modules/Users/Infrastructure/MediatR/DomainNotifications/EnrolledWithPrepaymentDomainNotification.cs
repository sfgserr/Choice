using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrolledWithPrepaymentDomainNotification : DomainNotificationBase<EnrolledWithPrepaymentDomainEvent>
    {
        public EnrolledWithPrepaymentDomainNotification(EnrolledWithPrepaymentDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}