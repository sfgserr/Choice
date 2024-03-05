
namespace Choice.EventBus.Messages.Events
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public UserCreatedEvent(string userGuid, string name, string email, string city, string street, string phoneNumber)
        {
            UserGuid = userGuid;
            Name = name;
            Email = email;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
        }

        public string UserGuid { get; }
        public string Name { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string City { get; }
        public string Street { get; }
    }
}
