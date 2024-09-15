using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Authentication.Authenticate
{
    public class AuthenticateCommand : ICommandWithResult<AuthenticationResult>
    {
        public AuthenticateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        
        public string Password { get; }
    }
}