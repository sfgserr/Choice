namespace Identity.Application.Authentication.Authenticate
{
    public class AuthenticationResult 
    {
        public AuthenticationResult(Guid userId)
        {
            UserId = userId;
        }

        public AuthenticationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        
        public Guid? UserId { get; }
        
        public string? ErrorMessage { get; }

        public bool IsSuccessful => UserId is not null;
    }
}