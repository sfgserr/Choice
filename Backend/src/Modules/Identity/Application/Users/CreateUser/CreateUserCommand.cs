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

        public Guid UserId { get; }

        public string Email { get; }
        
        public string Password { get; }

        public string PhoneNumber { get; }

        public string UserRole { get; }

        public string City { get; }

        public string Street { get; }
        
        public string Latitude { get; }
        
        public string Longitude { get; }
    }
}