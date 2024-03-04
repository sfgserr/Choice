
namespace Choice.EventBus.Messages.Events
{
    public class UserDataChangedEvent : IntegrationEvent
    {
        public UserDataChangedEvent(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }
        public string Surname { get; }
    }
}
