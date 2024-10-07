using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using Payments.Domain.EnrollmentPayments.Events;

namespace Payments.Infrastructure.MediatR.DomainNotifications
{
    internal class EnrollmentPaidDomainNotification : DomainNotificationBase<EnrollmentPaymentPaidDomainEvent>
    {
        public EnrollmentPaidDomainNotification(EnrollmentPaymentPaidDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}
