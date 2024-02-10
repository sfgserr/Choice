
namespace EventBus.Messages.Events
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public UserCreatedEvent(int userId, string password, string email = null, string phone = null)
        {
            UserId = userId;
            Password = password;
            Email = email ?? string.Empty;
            Phone = phone ?? string.Empty;
        }

        public int UserId { get; }
        public string Email { get; } = string.Empty;
        public string Phone { get; } = string.Empty;
        public string Password { get; } = string.Empty;
    }
}
