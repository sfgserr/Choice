using BuildingBlocks.Domain;
using Users.Domain.Users.Events;
using Users.Domain.Users.Rules;

namespace Users.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        private User()
        {

        }

        private User(
            UserId id, 
            string name, 
            string email, 
            string phoneNumber, 
            string hashedPassword, 
            string iconUri, 
            Address address, 
            UserRole role,
            IUsersCounter counter)
        {
            CheckRule(new UserEmailMustBeUniqueRule(counter, email));
            CheckRule(new UserPhoneNumberMustBeUniqueRule(counter, phoneNumber));

            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            HashedPassword = hashedPassword;
            IconUri = iconUri;
            Role = role;
            IsDataFilled = !role.Equals(UserRole.Company);
            Address = address;
            Role = role;

            AddDomainEvent(new UserCreatedDomainEvent(id));
        }

        internal static User Create(
            string name, 
            string email, 
            string phoneNumber, 
            string hashedPassword, 
            Address address,
            UserRole role,
            IUsersCounter counter)
        {
            return new User(
                new(Guid.NewGuid()), 
                name, 
                email, 
                phoneNumber, 
                hashedPassword,
                "defaulturi",
                address, 
                role,
                counter);
        }

        public UserId Id { get; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public string HashedPassword { get; private set; }

        public string IconUri { get; private set; }

        public bool IsDataFilled { get; private set; }

        public Address Address { get; private set; }

        public UserRole Role { get; private set; }

        internal void ChangeIconUri(string iconUri)
        {
            CheckRule(new FieldsMustBeProvidedRule([iconUri]));

            IconUri = iconUri;
        }

        internal void ChangeData(
            string name, 
            string email, 
            string phoneNumber, 
            Address address,
            IUsersCounter counter)
        {
            CheckRule(new FieldsMustBeProvidedRule([name, email, phoneNumber]));
            CheckRule(new UserEmailMustBeUniqueRule(counter, email));
            CheckRule(new UserPhoneNumberMustBeUniqueRule(counter, phoneNumber));

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        internal void FillData()
        {
            CheckRule(new UserRoleMustBeUserToFillDataRule(Role));

            IsDataFilled = true;

            Role = UserRole.Company;
        }
    }
}
