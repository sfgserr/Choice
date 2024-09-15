namespace Identity.Application.Authentication.Authenticate
{
    public class AuthenticationResult 
    {
        public AuthenticationResult(Guid userId)
        {
            UserId = userId;
            IsSuccessfull = true;
        }

        public AuthenticationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccessfull = false;
        }
        
        public Guid? UserId { get; }
        
        public string? ErrorMessage { get; }
        
        public bool IsSuccessfull { get; }
    }
}