using BuildingBlocks.Domain;
using Payments.Domain.Payers;

namespace Payments.Domain.SubscritpionPayments.Rules
{
    internal class CannotBuyPaymentWithActiveSubscriptionRule : IBusinessRule
    {
        private readonly bool _subscribe;
        private readonly PayerId _payerId;

        internal CannotBuyPaymentWithActiveSubscriptionRule(bool subscribe, PayerId payerId)
        {
            _subscribe = subscribe;
            _payerId = payerId;
        }

        public bool IsBroken => _subscribe;

        public string Message { get; } = "У вас уже есть подписка";
    }
}
