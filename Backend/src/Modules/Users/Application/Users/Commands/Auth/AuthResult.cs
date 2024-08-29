using Users.Domain.Users;

namespace Users.Application.Users.Commands.Auth
{
    public class AuthResult
    {
        public AuthResult(Guid userId)
        {
            UserId = userId;
            IsSuccessfull = true;
        }

        public AuthResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccessfull = false;
        }

        public Guid? UserId { get; }

        public string? ErrorMessage { get; }

        public bool IsSuccessfull { get; }
    }
}
