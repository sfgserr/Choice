namespace WebApi.Modules.Users.Companies
{
    public class CreateCompanyRequest
    {
        public CreateCompanyRequest(
            string name, 
            string password, 
            string email, 
            string phoneNumber, 
            string city, 
            string street,
            string deviceName,
            string  deviceToken)
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