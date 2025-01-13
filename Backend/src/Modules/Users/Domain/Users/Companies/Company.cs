using BuildingBlocks.Domain;
using Users.Domain.Categories;
using Users.Domain.Users.Companies.Rules;
using Users.Domain.Users.Rules;

namespace Users.Domain.Users.Companies
{
    public class Company : Entity, IAggregateRoot
    {
        private readonly List<string> _photoUris = [];
        private readonly List<string> _socialMediaUris = [];
        private readonly List<int> _categories = [];

        private Company()
        {

        }

        private Company(User user)
        {
            Id = new(user.Id.Value);
            UserId = user.Id;
            User = user;
        }

        public static Company Create(
            string name,
            string email,
            string phoneNumber,
            string password,
            Address address,
            IUsersCounter counter)
        {
            User user = User.Create(
                name,
                email,
                password,
                phoneNumber,
                address,
                UserRole.User,
                counter);

            return new Company(user);
        }

        public CompanyId Id { get; }
        
        public UserId UserId { get; }
        
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
            List<int> categories,
            List<string> photoUris,
            List<string> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            CheckRule(new CannotChangeDataWhenDataIsNotFilledRule(IsDataFilled));
            CheckRule(new FieldsMustBeProvidedRule([description]));
            CheckRule(new CategoriesCannotBeEmptyRule(categories));
            CheckRule(new AtLeastOneLinkToSocialMediaMustBeProvidedRule(socialMediaUris));
            
            IsPrepaymentAvailable = isPrepaymentAvailable;

            _photoUris.Clear();
            _photoUris.AddRange(photoUris);

            _categories.Clear();
            _categories.AddRange(categories);
            
            _socialMediaUris.Clear();
            _socialMediaUris.AddRange(socialMediaUris);
            
            User.ChangeData(
                name,
                email,
                phoneNumber,
                address,
                counter);
        }

        public void FillData(
            string description,
            List<int> categories,
            List<string> photoUris,
            List<string> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            CheckRule(new FieldsMustBeProvidedRule([description]));
            CheckRule(new CategoriesCannotBeEmptyRule(categories));
            CheckRule(new AtLeastOneLinkToSocialMediaMustBeProvidedRule(socialMediaUris));
            
            Description = description;
            IsPrepaymentAvailable = isPrepaymentAvailable;

            _photoUris.AddRange(photoUris);
            _categories.AddRange(categories);
            _socialMediaUris.AddRange(socialMediaUris);
            
            User.FillData();
        }

        public List<string> GetPhotoUris() =>
            _photoUris;

        public List<int> GetCategories() =>
            _categories;

        public List<string> GetSocialMediaUris() =>
            _socialMediaUris;
    }
}
