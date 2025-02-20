using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotFinishOrCancelIfUserIsNotEnrolledRule : IBusinessRule
    {
        private readonly bool _isEnrolled;

        internal CannotFinishOrCancelIfUserIsNotEnrolledRule(bool isEnrolled)
        {
            _isEnrolled = isEnrolled;
        }

        public bool IsBroken => !_isEnrolled;

        public string Message => "You are not enrolled";
    }
}
