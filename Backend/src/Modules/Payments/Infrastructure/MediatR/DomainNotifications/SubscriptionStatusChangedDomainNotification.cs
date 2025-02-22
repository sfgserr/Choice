using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Payments.Domain.Subscriptions.Events;

namespace Payments.Infrastructure.MediatR.DomainNotifications
{
    internal class SubscriptionStatusChangedDomainNotification : DomainNotificationBase<SubscriptionStatusChangedDomainEvent>
    {
        public SubscriptionStatusChangedDomainNotification(SubscriptionStatusChangedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}