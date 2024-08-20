using BuildingBlocks.Domain;
using Payments.Domain.Payers;

namespace Payments.Domain.SubscritpionPayments.Rules
{
    internal class CannotBuyPaymentWithActiveSubscriptionRule : IBusinessRule
    {
        private readonly ISubscriptionsCounter _subscriptionsCounter;
        private readonly PayerId _payerId;

        internal CannotBuyPaymentWithActiveSubscriptionRule(ISubscriptionsCounter subscriptionsCounter, PayerId payerId)
        {
            _subscriptionsCounter = subscriptionsCounter;
            _payerId = payerId;
        }

        public bool IsBroken => _subscriptionsCounter.GetSubscriptionCounter(_payerId) > 0;

        public string Message { get; } = "You have active subscriptions or payments";
    }
}
