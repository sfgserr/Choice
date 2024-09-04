using Users.Domain.Users;

namespace Users.Application.Users.Commands.Auth
{
    public class AuthResult
    {
        public AuthResult(Guid userId, string role)
        {
            UserId = userId;
            Role = role;
            IsSuccessfull = true;
        }

        public AuthResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccessfull = false;
        }

        public Guid? UserId { get; }
        
        public string? Role { get; }
        
        public string? ErrorMessage { get; }

        public bool IsSuccessfull { get; }
    }
}
