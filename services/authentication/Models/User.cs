
namespace Choice.Authentication.Models
{
    public class User
    {
        public User(Guid id, string email, string password, string phoneNumber)
        {
            Id = id;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        private User() {  }

        public Guid Id { get; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
