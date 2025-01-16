using BuildingBlocks.Domain;
using Payments.Domain.Payers;

namespace Payments.Domain.SubscriptionPayments.Events
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
