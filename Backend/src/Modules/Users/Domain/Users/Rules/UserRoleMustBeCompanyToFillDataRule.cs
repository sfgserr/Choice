using BuildingBlocks.Domain;

namespace Users.Domain.Users.Rules
{
    internal class UserRoleMustBeCompanyToFillDataRule : IBusinessRule
    {
        private readonly UserRole _role;

        internal UserRoleMustBeCompanyToFillDataRule(UserRole role)
        {
            _role = role;
        }

        public bool IsBroken => !_role.Equals(UserRole.User);

        public string Message { get; } = "Вы уже заполнили информацию";
    }
}
