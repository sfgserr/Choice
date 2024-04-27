
namespace Choice.EventBus.Messages.Events
{
    public class UserIconUriChangenEvent
    {
        public UserIconUriChangenEvent(string guid, string iconUri)
        {
            Guid = guid;
            IconUri = iconUri;
        }

        public string Guid { get; }
        public string IconUri { get; }
    }
}
