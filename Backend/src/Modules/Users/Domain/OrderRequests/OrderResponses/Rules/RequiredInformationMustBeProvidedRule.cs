using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class RequiredInformationMustBeProvidedRule : IBusinessRule
    {
        private readonly double _price;
        private readonly double _deadline;
        private readonly DateTime? _enrollmentDate;
        private readonly bool _toKnowPrice;
        private readonly bool _toKnowDeadline;
        private readonly bool _toKnowEnrollmentDate;

        internal RequiredInformationMustBeProvidedRule(
            double price, 
            double deadline, 
            DateTime? enrollmentDate, 
            bool toKnowPrice, 
            bool toKnowDeadline, 
            bool toKnowEnrollmentDate)
        {
            _price = price;
            _deadline = deadline;
            _enrollmentDate = enrollmentDate;
            _toKnowPrice = toKnowPrice;
            _toKnowDeadline = toKnowDeadline;
            _toKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public bool IsBroken => 
            (_toKnowPrice && _price == 0) || 
            (_toKnowDeadline && _deadline == 0) || 
            (_toKnowEnrollmentDate && _enrollmentDate == null);

        public string Message { get; } = "At least 1 requirement must be filled";
    }
}
