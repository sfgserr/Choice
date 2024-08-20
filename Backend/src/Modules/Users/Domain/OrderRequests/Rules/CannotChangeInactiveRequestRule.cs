using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.Rules
{
    internal class CannotChangeInactiveRequestRule : IBusinessRule
    {
        private readonly OrderStatus _requestStatus;

        internal CannotChangeInactiveRequestRule(OrderStatus requestStatus)
        {
            _requestStatus = requestStatus;
        }

        public bool IsBroken => !_requestStatus.Equals(OrderStatus.Active);

        public string Message => "Order is inactive";
    }
}
