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
            string deviceName,
            string deviceToken,
            Address address)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Role = role;
            DeviceName = deviceName;
            DeviceToken = deviceToken;
            Address = address;
        }

        public UserId UserId { get; }

        public string UserName { get; }
        
        public string Email { get; }
        
        public string Password { get; }

        public string PhoneNumber { get; }

        public string Role { get; }
        
        public string DeviceName { get; }
        
        public string DeviceToken { get; }

        public Address Address { get; }
    }
}
