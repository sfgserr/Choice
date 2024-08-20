using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderResponses.Rules
{
    internal class CannotChangeEnrollmentDateTwiceInARowRule : IBusinessRule
    {
        private readonly UserId? _userChangedEnrollmentDate;
        private readonly UserId _userChangingEnrollmentDate;

        internal CannotChangeEnrollmentDateTwiceInARowRule(
            UserId? userChangedEnrollmentDate, 
            UserId userChangingEnrollmentDate)
        {
            _userChangedEnrollmentDate = userChangedEnrollmentDate;
            _userChangingEnrollmentDate = userChangingEnrollmentDate;
        }

        public bool IsBroken => 
            _userChangedEnrollmentDate != null && _userChangedEnrollmentDate.Equals(_userChangingEnrollmentDate);

        public string Message => "You've already changed the enrollment date";
    }
}
