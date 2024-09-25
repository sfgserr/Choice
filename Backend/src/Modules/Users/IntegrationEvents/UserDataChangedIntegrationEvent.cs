using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class UserDataChangedIntegrationEvent : IntegrationEventBase
    {
        public UserDataChangedIntegrationEvent(
            Guid id,
            Guid userId,
            string name,
            string email,
            string phoneNumber,
            string city,
            string street,
            string latitude,
            string longitude) : base(id)
        {
            UserId = userId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }

        public string Street { get; }

        public string Latitude { get; }

        public string Longitude { get; }
    }
}
