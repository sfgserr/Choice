using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Payments.Domain.EnrollmentPayments.Events;

namespace Payments.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrollmentPaymentPaidDomainNotification : DomainNotificationBase<EnrollmentPaymentPaidDomainEvent>
    {
        public EnrollmentPaymentPaidDomainNotification(EnrollmentPaymentPaidDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
