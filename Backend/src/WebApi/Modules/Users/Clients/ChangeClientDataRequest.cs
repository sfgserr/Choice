namespace WebApi.Modules.Users.Clients
{
    public class ChangeClientDataRequest
    {
        public ChangeClientDataRequest(string name, string email, string phoneNumber, string city, string street)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }

        public string Street { get; }
    }
}
