namespace CompanyService.Api.ViewModels.Requests
{
    public class ChangeDataRequest
    {
        public ChangeDataRequest(string title, string email, string phoneNumber, string street, string city, 
            string siteUrl, List<string> photoUris, List<string> socialMedias, List<int> categoriesId)
        {
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

        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string SiteUrl { get; set; }
        public List<string> PhotoUris { get; set; }
        public List<string> SocialMedias { get; set; }
        public List<int> CategoriesId { get; set; }
    }
}
