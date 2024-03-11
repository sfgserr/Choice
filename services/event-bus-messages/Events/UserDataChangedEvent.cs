
namespace Choice.EventBus.Messages.Events
{
    public class UserDataChangedEvent : IntegrationEvent
    {
        public UserDataChangedEvent(string guid, string name, string email, string phoneNumber, string iconUri)
        {
            Guid = guid;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            IconUri = iconUri;
        }

        public string Guid { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string IconUri { get; }
    }
}
