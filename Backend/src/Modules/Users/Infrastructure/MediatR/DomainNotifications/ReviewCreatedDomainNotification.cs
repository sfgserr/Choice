using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Users.Domain.OrderRequests.OrderResponses.Events;

namespace Users.Infrastructure.MediatR.DomainNotifications
{
    internal class ReviewCreatedDomainNotification : DomainNotificationBase<ReviewCreatedDomainEvent>
    {
        public ReviewCreatedDomainNotification(ReviewCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}