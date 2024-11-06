using BuildingBlocks.Domain;

namespace Payments.Domain.EnrollmentPayments.Events
{
    public class EnrollmentPaymentExpiredDomainEvent : DomainEventBase
    {
        public EnrollmentPaymentExpiredDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }
    }
}