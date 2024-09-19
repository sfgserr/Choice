using BuildingBlocks.Domain;

namespace Users.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserCreatedDomainEvent(
            UserId userId, 
            string email, 
            string password, 
            string phoneNumber, 
            UserRole userRole, 
            Address address)
        {
            UserId = userId;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
            Address = address;
        }

        public UserId UserId { get; }

        public string Email { get; }
        
        public string Password { get; }

        public string PhoneNumber { get; }

        public UserRole UserRole { get; }

        public Address Address { get; }
    }
}
