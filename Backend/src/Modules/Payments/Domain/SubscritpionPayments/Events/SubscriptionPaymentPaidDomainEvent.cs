using BuildingBlocks.Domain;
using Payments.Domain.Payers;

namespace Payments.Domain.SubscritpionPayments.Events
{
    public class SubscriptionPaymentPaidDomainEvent : DomainEventBase
    {
        public SubscriptionPaymentPaidDomainEvent(PayerId payerId)
        {
            PayerId = payerId;
        }

        public PayerId PayerId { get; }
    }
}
