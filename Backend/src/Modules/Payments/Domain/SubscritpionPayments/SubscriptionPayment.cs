using BuildingBlocks.Domain;
using Payments.Domain.Payers;
using Payments.Domain.SeedWork;
using Payments.Domain.Subscriptions;
using Payments.Domain.SubscritpionPayments.Events;
using Payments.Domain.SubscritpionPayments.Rules;

namespace Payments.Domain.SubscritpionPayments
{
    public class SubscriptionPayment : Entity, IAggregateRoot
    {
        private SubscriptionPayment()
        {

        }

        private SubscriptionPayment(
            SubscrtipionPaymentId id,
            SubscriptionPeriod period,
            PayerId payerId,
            DateTime expirationDate,
            PaymentStatus status,
            ISubscriptionsCounter counter)
        {
            CheckRule(new CannotBuyPaymentWithActiveSubscriptionRule(counter, payerId));

            Id = id;
            Period = period;
            PayerId = payerId;
            ExpirationDate = expirationDate;
            Status = status;
        }

        public static SubscriptionPayment Buy(
            PayerId payerId, 
            SubscriptionPeriod period,
            ISubscriptionsCounter counter)
        {
            return new SubscriptionPayment(
                new(Guid.NewGuid()),
                period,
                payerId,
                DateCalculator.CalculateExpirationDateForPayment(),
                PaymentStatus.WaitingForPayment,
                counter);
        }

        public void Pay()
        {
            Status = PaymentStatus.WaitingForPayment;

            AddDomainEvent(new SubscriptionPaymentPaidDomainEvent(PayerId));
        }
        
        public SubscrtipionPaymentId Id { get; }

        public SubscriptionPeriod Period { get; }

        public PayerId PayerId { get; }

        public DateTime ExpirationDate { get; }

        public PaymentStatus Status { get; private set; }
    }
}
