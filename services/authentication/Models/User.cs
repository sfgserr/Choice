
using Microsoft.AspNetCore.Identity;

namespace Choice.Authentication.Api.Models
{
    public class User : IdentityUser
    {
        public User(string id, string email, string name, string phoneNumber, string city,
            string street, UserType userType)
        {
            Id = id;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            UserType = userType;
        }

        private User() {  }

        public string Name { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public UserType UserType { get; private set; }

        public void ChangeData(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }

    public enum UserType
    {
        Client = 1,
        Company = 2,
        Admin = 3
    }
}
