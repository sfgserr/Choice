using BuildingBlocks.Domain;
using Users.Domain.Categories;
using Users.Domain.Users.Companies.Rules;
using Users.Domain.Users.Rules;

namespace Users.Domain.Users.Companies
{
    public class Company : Entity, IAggregateRoot
    {
        private readonly List<string> _photoUris = [];
        private readonly List<CategoryId> _categories = [];

        private Company()
        {

        }

        private Company(User user)
        {
            Id = new(user.Id.Value);
            User = user;
        }

        public static Company Create(
            string name,
            string email,
            string phoneNumber,
            string hashedPassword,
            Address address,
            IUsersCounter counter)
        {
            User user = User.Create(
                name,
                email,
                phoneNumber,
                hashedPassword,
                address,
                UserRole.User,
                counter);

            return new Company(user);
        }

        public CompanyId Id { get; }

        public User User { get; }

        public string Description { get; private set; } = string.Empty;

        public bool IsDataFilled => User.IsDataFilled;

        public bool IsPrepaymentAvailable { get; private set; } = false;

        public void ChangeIconUri(string iconUri)
        {
            User.ChangeIconUri(iconUri);
        }

        public void ChangeData(
            string name,
            string email,
            string phoneNumber,
            Address address,
            IUsersCounter counter,
            string description,
            List<CategoryId> categories,
            List<string> photoUris,
            bool isPrepaymentAvailable)
        {
            CheckRule(new CannotChangeDataWhenDataIsNotFilledRule(IsDataFilled));
            CheckRule(new FieldsMustBeProvidedRule([description]));
            CheckRule(new CategoriesCannotBeEmptyRule(categories));

            IsPrepaymentAvailable = isPrepaymentAvailable;

            _photoUris.Clear();
            _photoUris.AddRange(photoUris);

            _categories.Clear();
            _categories.AddRange(categories);

            User.ChangeData(
                name,
                email,
                phoneNumber,
                address,
                counter);
        }

        public void FillData(
            string description,
            List<CategoryId> categories,
            List<string> photoUris, 
            bool isPrepaymentAvailable)
        {
            CheckRule(new FieldsMustBeProvidedRule([description]));
            CheckRule(new CategoriesCannotBeEmptyRule(categories));

            Description = description;
            IsPrepaymentAvailable = isPrepaymentAvailable;

            _photoUris.AddRange(photoUris);
            _categories.AddRange(categories);

            User.FillData();
        }
    }
}
