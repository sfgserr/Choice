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
        private SubscriptionPeriod _period;

        private PayerId _payerId;

        private DateTime _expirationDate;

        private PaymentStatus _status;
        
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
            _period = period;
            _payerId = payerId;
            _expirationDate = expirationDate;
            _status = status;
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
            _status = PaymentStatus.Paid;

            AddDomainEvent(new SubscriptionPaymentPaidDomainEvent(_payerId, _period.Value));
        }

        public void Expire()
        {
            if (_expirationDate < DateTime.UtcNow) _status = PaymentStatus.Expired;
        }

        public SubscrtipionPaymentId Id { get; }
    }
}
