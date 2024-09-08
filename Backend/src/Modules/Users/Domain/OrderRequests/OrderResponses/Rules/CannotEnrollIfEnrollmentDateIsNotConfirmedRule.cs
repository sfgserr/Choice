using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotEnrollIfEnrollmentDateIsNotConfirmedRule : IBusinessRule
    {
        private readonly bool _isEnrollmentDateConfirmed;

        internal CannotEnrollIfEnrollmentDateIsNotConfirmedRule(bool isEnrollmentDateConfirmed)
        {
            _isEnrollmentDateConfirmed = isEnrollmentDateConfirmed;
        }

        public bool IsBroken => !_isEnrollmentDateConfirmed;

        public string Message => "Enrollment date is not confirmed";
    }
}
