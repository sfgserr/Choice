using Choice.EventBus.Messages.Events;

namespace EventBus.Messages.Events
{
    public class UserDeletedEvent : IntegrationEvent
    {
        public UserDeletedEvent(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
