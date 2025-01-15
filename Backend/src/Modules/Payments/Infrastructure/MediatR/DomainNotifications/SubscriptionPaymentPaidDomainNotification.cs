using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Payments.Domain.SubscritpionPayments.Events;

namespace Payments.Infrastructure.MediatR.DomainNotifications
{
    internal class SubscriptionPaymentPaidDomainNotification : DomainNotificationBase<SubscriptionPaymentPaidDomainEvent>
    {
        public SubscriptionPaymentPaidDomainNotification(SubscriptionPaymentPaidDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
    