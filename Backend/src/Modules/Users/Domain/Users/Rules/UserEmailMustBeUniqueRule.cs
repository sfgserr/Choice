using BuildingBlocks.Domain;

namespace Users.Domain.Users.Rules
{
    internal class UserEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly IUsersCounter _counter;
        private readonly string _email;

        internal UserEmailMustBeUniqueRule(IUsersCounter counter, string email)
        {
            _counter = counter;
            _email = email;
        }

        public bool IsBroken => _counter.CountUsersByEmail(_email) > 0;

        public string Message { get; } = "Пользователь с таким e-mail уже существует";
    }
}
