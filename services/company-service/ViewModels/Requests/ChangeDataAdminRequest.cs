using Microsoft.IdentityModel.Tokens;

namespace CompanyService.Api.ViewModels.Requests
{
    public class ChangeDataAdminRequest
    {
        public ChangeDataAdminRequest(string guid, string title, string email, string phoneNumber, string street, 
            string city, string siteUrl, List<string> photoUris, List<string> socialMedias, List<int> categoriesId)
        {
            Guid = guid;
            Title = title;
            Email = email;
            PhoneNumber = phoneNumber;
            Street = street;
            City = city;
            SiteUrl = siteUrl;
            PhotoUris = photoUris;
            SocialMedias = socialMedias;
            CategoriesId = categoriesId;
        }

        public string Guid { get; }
        public string Title { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Street { get; }
        public string City { get; }
        public string SiteUrl { get; }
        public List<string> PhotoUris { get; }
        public List<string> SocialMedias { get; }
        public List<int> CategoriesId { get; }

        public bool IsValid =>
            !string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Email) ||
            !string.IsNullOrEmpty(PhoneNumber) || !string.IsNullOrEmpty(Street) || !string.IsNullOrEmpty(City) ||
            !string.IsNullOrEmpty(SiteUrl) || !SocialMedias.IsNullOrEmpty() || !CategoriesId.IsNullOrEmpty();
    }
}
