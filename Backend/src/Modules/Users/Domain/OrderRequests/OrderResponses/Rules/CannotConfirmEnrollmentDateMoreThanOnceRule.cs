using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotConfirmEnrollmentDateMoreThanOnceRule : IBusinessRule
    {
        private readonly bool _isConfirmed;

        internal CannotConfirmEnrollmentDateMoreThanOnceRule(bool isConfirmed)
        {
            _isConfirmed = isConfirmed;
        }

        public bool IsBroken => _isConfirmed;

        public string Message { get; } = "You have already enrolled";
    }
}
