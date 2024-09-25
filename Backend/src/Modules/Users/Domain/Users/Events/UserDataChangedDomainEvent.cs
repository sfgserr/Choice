using BuildingBlocks.Domain;

namespace Users.Domain.Users.Events
{
    public class UserDataChangedDomainEvent : DomainEventBase
    {
        public UserDataChangedDomainEvent(
            UserId userId,
            string name,
            string email,
            string phoneNumber,
            Address address)
        {
            UserId = userId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public UserId UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public Address Address { get; }
    }
}
