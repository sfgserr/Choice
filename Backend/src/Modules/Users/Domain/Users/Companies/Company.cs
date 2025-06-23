using BuildingBlocks.Domain;
using Users.Domain.Users.Companies.Rules;
using Users.Domain.Users.Rules;

namespace Users.Domain.Users.Companies
{
    public class Company : Entity, IAggregateRoot
    {
        private string _description = string.Empty;
        
        private bool _isPrepaymentAvailable = false;
        
        private User _user;
        
        private UserId _userId;
        
        private readonly List<string> _photoUris = [];
        
        private readonly List<SocialMedia> _socialMedias = [];
        
        private readonly List<int> _categories = [];
        
        private Company()
        {

        }

        private Company(User user)
        {
            Id = new(user.Id.Value);
            
            _userId = user.Id;
            _user = user;
        }

        public static Company Create(
            string name,
            string email,
            string phoneNumber,
            string password,
            string deviceName,
            string deviceToken,
            Address address,
            IUsersCounter counter)
        {
            User user = User.Create(
                name,
                email,
                password,
                phoneNumber,
                deviceName,
                deviceToken,
                address,
                UserRole.User,
                counter);

            return new Company(user);
        }

        public CompanyId Id { get; }
        
        public void ChangeIconUri(string iconUri)
        {
            _user.ChangeIconUri(iconUri);
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
            List<SocialMedia> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            CheckRule(new CannotChangeDataWhenDataIsNotFilledRule(_user.IsDataFilled));
            CheckRule(new FieldsMustBeProvidedRule([description]));
            CheckRule(new CategoriesCannotBeEmptyRule(categories));
            CheckRule(new AtLeastOneLinkToSocialMediaMustBeProvidedRule(socialMediaUris));
            
            _isPrepaymentAvailable = isPrepaymentAvailable;

            _photoUris.Clear();
            _photoUris.AddRange(photoUris);

            _categories.Clear();
            _categories.AddRange(categories);
            
            _socialMedias.Clear();
            _socialMedias.AddRange(socialMediaUris);
            
            _user.ChangeData(
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
            List<SocialMedia> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            CheckRule(new FieldsMustBeProvidedRule([description]));
            CheckRule(new CategoriesCannotBeEmptyRule(categories));
            CheckRule(new AtLeastOneLinkToSocialMediaMustBeProvidedRule(socialMediaUris));
            
            _description = description;
            _isPrepaymentAvailable = isPrepaymentAvailable;

            _photoUris.AddRange(photoUris);
            _categories.AddRange(categories);
            _socialMedias.AddRange(socialMediaUris);
            
            _user.FillData();
        }
        
        public List<string> GetPhotoUris() =>
            _photoUris;

        public List<SocialMedia> GetSocialMedias() =>
            _socialMedias;
    }
}
