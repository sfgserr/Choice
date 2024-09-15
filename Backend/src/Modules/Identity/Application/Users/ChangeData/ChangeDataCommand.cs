using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Users.ChangeData
{
    public class ChangeDataCommand : InternalCommandBase
    {
        public ChangeDataCommand(
            Guid id,
            Guid userId,
            string email, 
            string phoneNumber, 
            string city, 
            string street, 
            string latitude, 
            string longitude) : base(id)
        {
            UserId = userId;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
        }
        
        public Guid UserId { get; }
        
        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }

        public string Street { get; }

        public string Latitude { get; }

        public string Longitude { get; }
    }
}