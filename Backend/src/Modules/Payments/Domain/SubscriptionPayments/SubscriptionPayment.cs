using BuildingBlocks.Domain;
using Payments.Domain.Payers;
using Payments.Domain.SeedWork;
using Payments.Domain.SubscriptionPayments.Events;
using Payments.Domain.SubscriptionPayments.Rules;
using Payments.Domain.Subscriptions;

namespace Payments.Domain.SubscriptionPayments
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
            bool subscribe,
            ISubscriptionPaymentsCounter counter)
        {
            CheckRule(new CannotBuyPaymentWithActiveSubscriptionRule(subscribe));
            CheckRule(new CannotBuyPaymentWithActiveSubscriptionPaymentRule(counter, payerId));
            
            Id = id;
            Period = period;
            PayerId = payerId;
            ExpirationDate = expirationDate;
            Status = status;
        }

        public static SubscriptionPayment Buy(
            PayerId payerId, 
            SubscriptionPeriod period,
            bool subscribe,
            ISubscriptionPaymentsCounter counter)
        {
            return new SubscriptionPayment(
                new(Guid.NewGuid()),
                period,
                payerId,
                DateCalculator.CalculateExpirationDateForPayment(),
                PaymentStatus.WaitingForPayment,
                subscribe,
                counter);
        }

        public void Pay()
        {
            Status = PaymentStatus.Paid;

            AddDomainEvent(new SubscriptionPaymentPaidDomainEvent(PayerId, Period.Value));
        }

        public void Expire()
        {
            Status = PaymentStatus.Expired;
        }

        public SubscrtipionPaymentId Id { get; }

        public SubscriptionPeriod Period { get; }

        public PayerId PayerId { get; }

        public DateTime ExpirationDate { get; }

        public PaymentStatus Status { get; private set; }
    }
}
