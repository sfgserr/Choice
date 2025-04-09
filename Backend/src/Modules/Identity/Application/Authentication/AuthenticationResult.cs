namespace Identity.Application.Authentication
{
    public class AuthenticationResult 
    {
        public AuthenticationResult(UserDto user)
        {
            User = user;
            IsSuccessful = true;
        }

        public AuthenticationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccessful = false;
        }
        
        public UserDto? User { get; }
        
        public string? ErrorMessage { get; }

        public bool IsSuccessful { get; }
    }
}