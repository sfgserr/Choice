using BuildingBlocks.Domain;
using Identity.Domain.Users.Rules;

namespace Identity.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        private User(
            UserId id, 
            string email,
            string hashedPassword,
            string phoneNumber,
            UserRole role, 
            Address address)
        {
            Id = id;
            Email = email;
            HashedPassword = hashedPassword;
            PhoneNumber = phoneNumber;
            Role = role;
            Address = address;
        }

        private User()
        {
            
        }

        public static User Create(
            UserId id, 
            string email, 
            string password,
            string phoneNumber,
            UserRole role,
            Address address)
        {
            return new User(
                id,
                email,
                PasswordManager.HashPassword(password),
                phoneNumber,
                role,
                address);
        }
        
        public UserId Id { get; }

        public string Email { get; private set; }

        public string HashedPassword { get; private set; }

        public string PhoneNumber { get; private set; }

        public UserRole Role { get; private set; }
        
        public Address Address { get; private set; }

        public void ChangePassword(string password)
        {
            CheckRule(new PasswordsMustBeEqualRule(HashedPassword, password));

            HashedPassword = PasswordManager.HashPassword(password);
        }

        public void ChangeData(string email, string phoneNumber, Address address)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }
    }
}