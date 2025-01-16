using BuildingBlocks.Domain;

namespace Payments.Domain.SubscriptionPayments.Rules
{
    internal class CannotBuyPaymentWithActiveSubscriptionRule : IBusinessRule
    {
        private readonly bool _subscribe;

        internal CannotBuyPaymentWithActiveSubscriptionRule(bool subscribe)
        {
            _subscribe = subscribe;
        }

        public bool IsBroken => _subscribe;

        public string Message { get; } = "У вас уже есть подписка";
    }
}
