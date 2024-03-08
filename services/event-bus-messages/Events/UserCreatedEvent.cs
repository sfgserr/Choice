namespace Choice.EventBus.Messages.Events
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public UserCreatedEvent(string userGuid, string name, string surname, string email, string city,
            string street, string phoneNumber, string userType)
        {
            UserGuid = userGuid;
            Name = name;
            Surname = surname;
            Email = email;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            UserType = userType;
        }

        public string UserGuid { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string City { get; }
        public string Street { get; }
        public string PhoneNumber { get; }
        public string UserType { get; }
    }
}
