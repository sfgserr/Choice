
namespace Choice.EventBus.Messages.Events
{
    public class UserDataChangedEvent : IntegrationEvent
    {
        public UserDataChangedEvent(string guid, string name, string email, string phoneNumber)
        {
            Guid = guid;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Guid { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
    }
}
