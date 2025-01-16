using BuildingBlocks.Domain;
using Payments.Domain.Payers;

namespace Payments.Domain.SubscriptionPayments.Rules
{
    internal class CannotBuyPaymentWithActiveSubscriptionPaymentRule : IBusinessRule
    {
        private readonly ISubscriptionPaymentsCounter _counter;
        private readonly PayerId _payerId;

        internal CannotBuyPaymentWithActiveSubscriptionPaymentRule(ISubscriptionPaymentsCounter counter, PayerId payerId)
        {
            _counter = counter;
            _payerId = payerId;
        }

        public bool IsBroken => _counter.Count(_payerId) > 0;
        
        public string Message => "You already have active subscription payment";
    }
}