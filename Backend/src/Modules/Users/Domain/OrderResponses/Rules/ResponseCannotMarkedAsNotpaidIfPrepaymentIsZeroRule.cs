using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Rules
{
    internal class ResponseCannotMarkedAsNotpaidIfPrepaymentIsZeroRule : IBusinessRule
    {
        private readonly bool _isPaid;
        private readonly double _prepayment;

        internal ResponseCannotMarkedAsNotpaidIfPrepaymentIsZeroRule(bool isPaid, double prepayment)
        {
            _isPaid = isPaid;
            _prepayment = prepayment;
        }

        public bool IsBroken => !_isPaid && _prepayment == 0;

        public string Message { get; } = "Prepayment available but the amount is 0";
    }
}
