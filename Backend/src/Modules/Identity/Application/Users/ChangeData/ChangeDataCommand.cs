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
        
        internal Guid UserId { get; }
        
        internal string Email { get; }

        internal string PhoneNumber { get; }

        internal string City { get; }

        internal string Street { get; }

        internal string Latitude { get; }

        internal string Longitude { get; }
    }
}