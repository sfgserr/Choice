namespace Choice.CompanyService.Api.ViewModels.Requests
{
    public class FillCompanyDataRequest
    {
        public FillCompanyDataRequest(string siteUrl, string street, string city,
            List<string> socialMedias, List<string> photoUris, List<int> categoriesId)
        {
            SiteUrl = siteUrl;
            Street = street;
            City = city;
            SocialMedias = socialMedias;
            PhotoUris = photoUris;
            CategoriesId = categoriesId;
        }

        public string SiteUrl { get; }
        public string Street { get; }
        public string City { get; }
        public List<string> SocialMedias { get; }
        public List<string> PhotoUris { get; }
        public List<int> CategoriesId { get; }
    }
}
