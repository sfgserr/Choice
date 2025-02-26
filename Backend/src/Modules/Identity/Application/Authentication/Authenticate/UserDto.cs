namespace Identity.Application.Authentication.Authenticate
{
    public class UserDto
    { 
        public Guid UserId { get; }

        public string UserType { get; }
        
        public bool IsSubscribed { get; }

        public string Password { get; }
    }
}