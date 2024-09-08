using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class ResponseCannotMarkedAsNotPaidIfPrepaymentIsZeroRule : IBusinessRule
    {
        private readonly bool _isPaid;
        private readonly double _prepayment;

        internal ResponseCannotMarkedAsNotPaidIfPrepaymentIsZeroRule(bool isPaid, double prepayment)
        {
            _isPaid = isPaid;
            _prepayment = prepayment;
        }

        public bool IsBroken => !_isPaid && _prepayment == 0;

        public string Message { get; } = "Prepayment available but the amount is 0";
    }
}
