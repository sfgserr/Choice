namespace Identity.Application.Authentication
{
    public class UserDto
    { 
        public Guid UserId { get; }

        public string UserType { get; }
        
        public bool IsSubscribed { get; }

        public string Password { get; }
    }
}