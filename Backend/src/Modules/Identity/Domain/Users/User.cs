using BuildingBlocks.Domain;
using Identity.Domain.Users.Rules;

namespace Identity.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        private string _email;

        private string _password;

        private string _phoneNumber;

        private UserRole _role;

        private Address _address;

        private bool _isSubscribed;
        
        private User(
            UserId id, 
            string email,
            string password,
            string phoneNumber,
            UserRole role, 
            Address address,
            bool isSubscribed)
        {
            Id = id;
            _email = email;
            _password = password;
            _phoneNumber = phoneNumber;
            _role = role;
            _address = address;
            _isSubscribed = isSubscribed;    
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
                address,
                true);
        }
        
        public UserId Id { get; }
        
        public void ChangePassword(string oldPassword, string newPassword)
        {
            CheckRule(new PasswordsMustBeEqualRule(_password, oldPassword));

            _password = PasswordManager.HashPassword(newPassword);
        }
        
        public void ChangeData(string email, string phoneNumber, Address address)
        {
            _email = email;
            _phoneNumber = phoneNumber;
            _address = address;
        }

        public void ChangeRole(UserRole role)
        {
            _role = role;
        }

        public void ToggleSubscription()
        {
            _isSubscribed = !_isSubscribed;
        }
    }
}