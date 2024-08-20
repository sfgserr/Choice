using BuildingBlocks.Domain;
using Users.Domain.OrderRequests;

namespace Users.Domain.OrderResponses.Rules
{
    internal class CannotMakeOperationsWithNotActiveOrderRule : IBusinessRule
    {
        private readonly OrderStatus _status;
        private readonly bool _isActive;

        internal CannotMakeOperationsWithNotActiveOrderRule(OrderStatus status, bool isActive)
        {
            _status = status;
            _isActive = isActive;
        }

        public bool IsBroken => _status != OrderStatus.Active || !_isActive;

        public string Message { get; } = "Order is not active";
    }
}
