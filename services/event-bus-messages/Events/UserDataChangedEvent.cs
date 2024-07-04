
namespace Choice.EventBus.Messages.Events
{
    public class UserDataChangedEvent : IntegrationEvent
    {
        public UserDataChangedEvent(
            string guid,
            string name,
            string email,
            string phoneNumber,
            string city,
            string street)
        {
            Guid = guid;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        public string Guid { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string City { get; }
        public string Street { get; }
    }
}
