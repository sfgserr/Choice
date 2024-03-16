using Choice.Common.ValueObjects;

namespace Choice.CompanyService.Api.Entities
{
    public class Company
    {
        private readonly List<string> _socialMedias = new();
        private readonly List<string> _photoUris = new();
        private readonly List<int> _categoriesId = new();

        public Company(string guid, string title, string email, string phoneNumber, Address address)
        {
            Guid = guid;
            Title = title;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        private Company() { }

        public int Id { get; }
        public string Guid { get; }
        public string Title { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string SiteUrl { get; private set; } = string.Empty;
        public Address Address { get; private set; }    
        public double AverageGrade { get; private set; } = 0;
        public int ReviewCount { get; private set; } = 0;
        public IReadOnlyCollection<string> SocialMedias => _socialMedias.AsReadOnly();
        public IReadOnlyCollection<string> PhotoUris => _photoUris.AsReadOnly();
        public IReadOnlyCollection<int> CategoriesId => _categoriesId.AsReadOnly();

        public void FillCompanyData(string siteUrl, string street, string city,
            List<string> socialMedias, List<string> photoUris, List<int> categoriesId)
        {
            SiteUrl = siteUrl;
            Address = new(street, city);

            _socialMedias.AddRange(socialMedias);
            _photoUris.AddRange(photoUris);
            _categoriesId.AddRange(categoriesId);
        }

        public void AddReview(int grade)
        {
            AverageGrade = ReviewCount < 1 ? grade : (ReviewCount * AverageGrade + grade) / (ReviewCount + 1);
            ReviewCount++;
        }
    }
}
