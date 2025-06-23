using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IntegrationEventBase 
    {
        public UserCreatedIntegrationEvent(
            Guid id,
            Guid userId, 
            string userName,
            string email, 
            string password, 
            string phoneNumber, 
            string userRole, 
            string city, 
            string street, 
            string latitude, 
            string longitude,
            string deviceName,
            string deviceToken) : base(id)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
            DeviceName = deviceName;
            DeviceToken = deviceToken;
        }

        public Guid UserId { get; }
        
        public string UserName { get; }
        
        public string Email { get; }
        
        public string Password { get; }

        public string PhoneNumber { get; }

        public string UserRole { get; }

        public string City { get; }

        public string Street { get; }
        
        public string Latitude { get; }
        
        public string Longitude { get; }
        public string DeviceName { get; }
        public string DeviceToken { get; }
    }
}
