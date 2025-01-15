using BuildingBlocks.Domain;
using Payments.Domain.Payers;
using Payments.Domain.Subscriptions;

namespace Payments.Domain.SubscritpionPayments.Events
{
    public class SubscriptionPaymentPaidDomainEvent : DomainEventBase
    {
        public SubscriptionPaymentPaidDomainEvent(PayerId payerId, string periodName)
        {
            PayerId = payerId;
            PeriodName = periodName;
        }

        public PayerId PayerId { get; }
        
        public string PeriodName { get; }
    }
}
