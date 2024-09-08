using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotEnrollIfOrderIsNotPaidRule : IBusinessRule
    {
        private readonly bool _isPaid;

        internal CannotEnrollIfOrderIsNotPaidRule(bool isPaid)
        {
            _isPaid = isPaid;
        }

        public bool IsBroken => !_isPaid;

        public string Message { get; } = "Order is not paid";
    }
}
