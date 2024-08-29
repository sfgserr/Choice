namespace WebApi.Modules.Users
{
    public class LoginRequest
    {
        public LoginRequest(string email, string encodedPassword)
        {
            Email = email;
            EncodedPassword = encodedPassword;
        }

        public string Email { get; }

        public string EncodedPassword { get; }
    }
}
