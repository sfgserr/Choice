namespace Identity.Application.Authentication.Authenticate
{
    public class UserDto
    {
        public UserDto(Guid userId, string userType)
        {
            UserId = userId;
            UserType = userType;
        }

        public Guid UserId { get; }

        public string UserType { get; }
    }
}