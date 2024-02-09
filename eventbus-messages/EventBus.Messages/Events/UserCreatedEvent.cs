
namespace EventBus.Messages.Events
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
