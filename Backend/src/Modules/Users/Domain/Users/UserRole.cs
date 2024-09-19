using BuildingBlocks.Domain;

namespace Users.Domain.Users
{
    public class UserRole : ValueObject
    {
        private UserRole(string value)
        {
            Value = value;
        }
        
        private UserRole() {}
        
        public string Value { get; }

        public static UserRole Admin { get; } = new UserRole("Admin");

        public static UserRole Client { get; } = new UserRole("Client");

        public static UserRole Company { get; } = new UserRole("Company");

        public static UserRole User { get; } = new UserRole("User");

        public static UserRole Parse(string value) => value switch
        {
            "Admin" => Admin,
            "Client" => Client,
            "Company" => Company,
            "User" => User,
            _ => throw new ArgumentException("No such role")
        };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
