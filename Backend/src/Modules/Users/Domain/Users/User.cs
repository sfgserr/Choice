using BuildingBlocks.Domain;
using Users.Domain.Users.Events;
using Users.Domain.Users.Rules;

namespace Users.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        private string _name;

        private string _email;

        private string _phoneNumber;

        private string _iconUri;
        
        private Address _address;

        private UserRole _role;

        private int _reviewsCount;

        private double _averageGrade;
        
        private User()
        {

        }

        private User(
            UserId id, 
            string name, 
            string email,
            string password,
            string phoneNumber,  
            string iconUri, 
            Address address, 
            UserRole role,
            IUsersCounter counter)
        {
            CheckRule(new UserEmailMustBeUniqueRule(counter, email));
            CheckRule(new UserPhoneNumberMustBeUniqueRule(counter, phoneNumber));

            Id = id;
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            _iconUri = iconUri;
            _role = role;
            IsDataFilled = !role.Equals(UserRole.Company);
            _address = address;
            _role = role;

            AddDomainEvent(new UserCreatedDomainEvent(
                id,
                name,
                email,
                password,
                phoneNumber,
                role.Value,
                address));
        }

        internal static User Create(
            string name, 
            string email,
            string password,
            string phoneNumber,  
            Address address,
            UserRole role,
            IUsersCounter counter)
        {
            return new User(
                new(Guid.NewGuid()), 
                name, 
                email, 
                password,
                phoneNumber, 
                "default.png",
                address, 
                role,
                counter);
        }

        public UserId Id { get; }

        internal bool IsDataFilled { get; private set; }

        public void Review(double grade)
        {
            _averageGrade = (_averageGrade * _reviewsCount + grade) / ++_reviewsCount;
        }
        
        internal void ChangeIconUri(string iconUri)
        {
            CheckRule(new FieldsMustBeProvidedRule([iconUri]));

            _iconUri = iconUri;
            
            AddDomainEvent(new UserIconUriChangedDomainEvent(Id, _iconUri));
        }

        internal void ChangeData(
            string name, 
            string email, 
            string phoneNumber, 
            Address address,
            IUsersCounter counter)
        {
            CheckRule(new FieldsMustBeProvidedRule([name, email, phoneNumber]));
            
            if (_email != email)
                CheckRule(new UserEmailMustBeUniqueRule(counter, email));
            
            if (_phoneNumber != phoneNumber)
                CheckRule(new UserPhoneNumberMustBeUniqueRule(counter, phoneNumber));

            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
            _address = address;

            AddDomainEvent(new UserDataChangedDomainEvent(
                Id,
                _name,
                _email,
                _phoneNumber,
                _address));
        }

        internal void FillData()
        {
            CheckRule(new UserRoleMustBeCompanyToFillDataRule(_role));

            IsDataFilled = true;

            _role = UserRole.Company;

            AddDomainEvent(new UserRoleChangedDomainEvent(Id, _role.Value));
        }
    }
}
