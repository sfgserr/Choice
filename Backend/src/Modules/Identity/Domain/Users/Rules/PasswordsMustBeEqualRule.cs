using BuildingBlocks.Domain;

namespace Identity.Domain.Users.Rules
{
    internal class PasswordsMustBeEqualRule : IBusinessRule
    {
        private readonly string _hashedPassword;
        private readonly string _password;

        internal PasswordsMustBeEqualRule(string hashedPassword, string password)
        {
            _hashedPassword = hashedPassword;
            _password = password;
        }

        public bool IsBroken => !PasswordManager.VerifyHashedPassword(_hashedPassword, _password);

        public string Message => "Passwords are not equal";
    }
}