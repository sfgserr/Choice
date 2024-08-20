using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Rules
{
    internal class CannotChangeEnrollmentDateIfUserEnrolledRule : IBusinessRule
    {
        private readonly bool _isEnrolled;

        internal CannotChangeEnrollmentDateIfUserEnrolledRule(bool isEnrolled)
        {
            _isEnrolled = isEnrolled;
        }

        public bool IsBroken => _isEnrolled;

        public string Message { get; } = "User already enrolled";
    }
}
