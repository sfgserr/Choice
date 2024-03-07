
namespace Choice.EventBus.Messages.Events
{
    public class ClientCreatedEvent : IntegrationEvent
    {
        public ClientCreatedEvent(string userGuid, string name, string surname, string email, string city, string street)
        {
            UserGuid = userGuid;
            Name = name;
            Surname = surname;
            Email = email;
            City = city;
            Street = street;
        }

        public string UserGuid { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string City { get; }
        public string Street { get; }
    }
}
