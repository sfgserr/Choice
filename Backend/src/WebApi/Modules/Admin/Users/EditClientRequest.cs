namespace WebApi.Modules.Admin.Users
{
    public class EditClientRequest
    {
        public EditClientRequest(
            Guid clientId, 
            string iconUri,  
            string name, 
            string phoneNumber, 
            string email, 
            string city, 
            string street)
        {
            ClientId = clientId;
            IconUri = iconUri;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            City = city;
            Street = street;
        }

        public Guid ClientId { get; }
        
        public string IconUri { get; }

        public string Name { get; }

        public string PhoneNumber { get; }
        
        public string Email { get; }

        public string City { get; }

        public string Street { get; }
    }
}