using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Payments.Domain.EnrollmentPayments.Events;

namespace Payments.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrollmentExpiredDomainNotification : DomainNotificationBase<EnrollmentPaymentExpiredDomainEvent>
    {
        public EnrollmentExpiredDomainNotification(EnrollmentPaymentExpiredDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}