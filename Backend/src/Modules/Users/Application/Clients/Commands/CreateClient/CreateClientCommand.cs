using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : ICommand
    {
        public CreateClientCommand(
            string name, 
            string password, 
            string email, 
            string phoneNumber, 
            string city, 
            string street,
            string deviceName,
            string deviceToken)
        {
            Name = name;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            DeviceName = deviceName;
            DeviceToken = deviceToken;
        }

        public string Name { get; }

        public string Password { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }

        public string Street { get; }
        
        public string DeviceName { get; }
        
        public string DeviceToken { get; }
    }
}
