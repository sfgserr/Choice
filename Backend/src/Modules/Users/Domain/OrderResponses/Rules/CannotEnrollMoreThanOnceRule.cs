using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Rules
{
    internal class CannotEnrollMoreThanOnceRule : IBusinessRule
    {
        private readonly bool _isEnrolled;

        internal CannotEnrollMoreThanOnceRule(bool isEnrolled)
        {
            _isEnrolled = isEnrolled;
        }

        public bool IsBroken => !_isEnrolled;

        public string Message => "You are already enrolled";
    }
}
