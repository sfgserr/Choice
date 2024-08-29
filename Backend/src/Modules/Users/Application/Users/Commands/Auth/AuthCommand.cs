using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Users.Commands.Auth
{
    public class AuthCommand : ICommandWithResult<AuthResult>
    {
        public AuthCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
