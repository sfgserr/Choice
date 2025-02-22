namespace Identity.Application.Authentication.Authenticate
{
    public class UserDto
    {
        public UserDto(Guid userId, string userType, bool isSubscribed)
        {
            UserId = userId;
            UserType = userType;
            IsSubscribed = isSubscribed;
        }

        public Guid UserId { get; }

        public string UserType { get; }
        
        public bool IsSubscribed { get; }
    }
}