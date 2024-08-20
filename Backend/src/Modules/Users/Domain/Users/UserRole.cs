using BuildingBlocks.Domain;

namespace Users.Domain.Users
{
    public class UserRole : ValueObject
    {
        private UserRole(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static UserRole Admin { get; } = new UserRole("Admin");

        public static UserRole Client { get; } = new UserRole("Client");

        public static UserRole Company { get; } = new UserRole("Company");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
