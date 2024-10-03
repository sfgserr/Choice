using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Users.CreateUser
{
    public class CreateUserCommand : InternalCommandBase
    {
        public CreateUserCommand(
            Guid id,
            Guid userId, 
            string email, 
            string password, 
            string phoneNumber, 
            string userRole, 
            string city, 
            string street, 
            string latitude, 
            string longitude) : base(id)
        {
            UserId = userId;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
        }

        internal Guid UserId { get; }

        internal string Email { get; }
        
        internal string Password { get; }

        internal string PhoneNumber { get; }

        internal string UserRole { get; }

        internal string City { get; }

        internal string Street { get; }
        
        internal string Latitude { get; }
        
        internal string Longitude { get; }
    }
}