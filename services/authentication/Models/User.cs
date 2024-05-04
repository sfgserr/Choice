using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Choice.Authentication.Api.Models
{
    public class User : IdentityUser
    {
        public User(string id, string email, string userName, string phoneNumber, string city,
            string street, UserType userType)
        {
            Id = id;
            Email = email;
            UserName = userName;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            UserType = userType;
            IsDataFilled = userType != UserType.Company;
        }

        [Required]
        public string City { get; private set; }

        [Required]
        public string Street { get; private set; }

        [Required]
        public UserType UserType { get; private set; }

        [Required]
        public bool IsDataFilled { get; private set; }

        public void ChangeData(string name, string email, string phoneNumber)
        {
            UserName = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void SetDataFilled()
        {
            IsDataFilled = true;
        }
    }

    public enum UserType
    {
        Client = 1,
        Company = 2,
        Admin = 3
    }
}
