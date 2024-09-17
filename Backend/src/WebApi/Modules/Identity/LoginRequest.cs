namespace WebApi.Modules.Identity
{
    public class LoginRequest
    {
        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}