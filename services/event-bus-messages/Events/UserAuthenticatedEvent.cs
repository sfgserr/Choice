using Choice.EventBus.Messages.Events;

namespace EventBus.Messages.Events
{
    public class UserAuthenticatedEvent : IntegrationEvent
    {
        public UserAuthenticatedEvent(string userId, string deviceToken)
        {
            UserId = userId;
            DeviceToken = deviceToken;
        }

        public string UserId { get; }
        public string DeviceToken { get; }
    }
}
