namespace Choice.EventBus.Messages.Events
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public UserCreatedEvent(string userGuid, string name, string email, string city,
            string street, string phoneNumber, string iconUri, string userType)
        {
            UserGuid = userGuid;
            Name = name;
            Email = email;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            IconUri = iconUri;
            UserType = userType;
        }

        public string UserGuid { get; }
        public string Name { get; }
        public string Email { get; }
        public string City { get; }
        public string Street { get; }
        public string PhoneNumber { get; }
        public string IconUri { get; }
        public string UserType { get; }
    }
}
