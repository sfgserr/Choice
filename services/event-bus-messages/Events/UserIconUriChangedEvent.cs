
namespace Choice.EventBus.Messages.Events
{
    public class UserIconUriChangedEvent
    {
        public UserIconUriChangedEvent(string guid, string iconUri)
        {
            Guid = guid;
            IconUri = iconUri;
        }

        public string Guid { get; }
        public string IconUri { get; }
    }
}
