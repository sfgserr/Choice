
namespace Choice.Authentication.Models
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
            UserType = userType;
        }

        private User() {  }

        public Guid Id { get; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string City { get; }
        public string Street { get; }
        public UserType UserType { get; }
    }

    public enum UserType
    {
        Client = 1,
        Company = 2
    }
}
