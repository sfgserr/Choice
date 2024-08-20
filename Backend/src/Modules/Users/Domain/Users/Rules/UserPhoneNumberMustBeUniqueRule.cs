using BuildingBlocks.Domain;

namespace Users.Domain.Users.Rules
{
    internal class UserPhoneNumberMustBeUniqueRule : IBusinessRule
    {
        private readonly IUsersCounter _counter;
        private readonly string _phoneNumber;

        internal UserPhoneNumberMustBeUniqueRule(IUsersCounter counter, string phoneNumber)
        {
            _counter = counter;
            _phoneNumber = phoneNumber;
        }

        public bool IsBroken => _counter.CountUsersByPhoneNumber(_phoneNumber) > 0;

        public string Message { get; } = "User with such phone number already exists";
    }
}
