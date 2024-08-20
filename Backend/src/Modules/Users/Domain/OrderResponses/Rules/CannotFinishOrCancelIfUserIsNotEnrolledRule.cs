using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Rules
{
    internal class CannotFinishOrCancelIfUserIsNotEnrolledRule : IBusinessRule
    {
        private readonly bool _isEnrolled;
        private readonly bool _isPaid;

        internal CannotFinishOrCancelIfUserIsNotEnrolledRule(bool isEnrolled, bool isPaid)
        {
            _isEnrolled = isEnrolled;
            _isPaid = isPaid;
        }

        public bool IsBroken => !_isEnrolled || !_isPaid;

        public string Message => "You are not enrolled";
    }
}
