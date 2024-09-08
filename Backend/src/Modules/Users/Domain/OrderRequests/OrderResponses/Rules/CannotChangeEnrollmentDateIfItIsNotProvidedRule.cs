using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotChangeEnrollmentDateIfItIsNotProvidedRule : IBusinessRule
    {
        private readonly DateTime? _enrollmentDate;

        internal CannotChangeEnrollmentDateIfItIsNotProvidedRule(DateTime? enrollmentDate)
        {
            _enrollmentDate = enrollmentDate;
        }

        public bool IsBroken => _enrollmentDate is null;

        public string Message => "Enrollment Date is not provided";
    }
}
