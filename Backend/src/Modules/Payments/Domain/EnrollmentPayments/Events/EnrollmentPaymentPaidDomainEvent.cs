using BuildingBlocks.Domain;

namespace Payments.Domain.EnrollmentPayments.Events
{
    public class EnrollmentPaymentPaidDomainEvent : DomainEventBase
    {
        public EnrollmentPaymentPaidDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}
