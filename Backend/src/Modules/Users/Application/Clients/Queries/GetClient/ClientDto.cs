
namespace Users.Application.Clients.Queries.GetClient
{
    public class ClientDto
    {
        public ClientDto(
            Guid id, 
            string iconUri, 
            string name, 
            string email, 
            string phoneNumber, 
            string city, 
            string street)
        {
            Id = id;
            IconUri = iconUri;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        public Guid Id { get; }

        public string IconUri { get; }

        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }

        public string Street { get; }
    }
}
