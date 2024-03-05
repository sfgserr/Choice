
namespace Choice.Authentication.Models
{
    public class User
    {
        public User(Guid id, string email, string password, string name, string phoneNumber, string city
            string street)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        private User() {  }

        public Guid Id { get; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string City { get; }
        public string Street { get; }
    }
}
