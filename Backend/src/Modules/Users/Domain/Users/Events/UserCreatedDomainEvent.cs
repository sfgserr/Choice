using BuildingBlocks.Domain;

namespace Users.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserCreatedDomainEvent(
            UserId userId, 
            string userName,
            string email, 
            string password, 
            string phoneNumber, 
            string role, 
            Address address)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Role = role;
            Address = address;
        }

        public UserId UserId { get; }

        public string UserName { get; }
        
        public string Email { get; }
        
        public string Password { get; }

        public string PhoneNumber { get; }

        public string Role { get; }

        public Address Address { get; }
    }
}
