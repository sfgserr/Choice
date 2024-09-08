using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotReviewWhileOrderActive : IBusinessRule
    {
        private readonly OrderStatus _status;

        internal CannotReviewWhileOrderActive(OrderStatus status)
        {
            _status = status;
        }

        public bool IsBroken => _status.Equals(OrderStatus.Active);

        public string Message { get; } = "Order is not finished";
    }
}
