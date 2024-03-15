
namespace Choice.Authentication.Api.Models
{
    public class User
    {
        public User(Guid id, string email, string password, string name, string phoneNumber, string city,
            string street, UserType userType)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            IconUri = "defaulturi";
            UserType = userType;
        }

        private User() {  }

        public Guid Id { get; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string IconUri { get; private set; }
        public UserType UserType { get; private set; }

        public void ChangeData(string name, string email, string phoneNumber, string iconUri)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            IconUri = iconUri;
        }
    }

    public enum UserType
    {
        Client = 1,
        Company = 2,
        Admin = 3
    }
}
