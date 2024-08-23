using BuildingBlocks.Application.Cqrs.Commands;

namespace Application.Users.Clients.Commands.Create
{
    public class CreateClientCommand : ICommand
    {
        public CreateClientCommand(
            string name, 
            string password, 
            string email, 
            string phoneNumber, 
            string city, 
            string street)
        {
            Name = name;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        public string Name { get; }

        public string Password { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }

        public string Street { get; }
    }
}
