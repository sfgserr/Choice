using Microsoft.IdentityModel.Tokens;

namespace Choice.CompanyService.Api.ViewModels.Requests
{
    public class FillCompanyDataRequest
    {
        public FillCompanyDataRequest(string siteUrl, List<string> socialMedias, List<string> photoUris, 
            List<int> categoriesId, bool prepaymentAvailable)
        {
            SiteUrl = siteUrl;
            SocialMedias = socialMedias;
            PhotoUris = photoUris;
            CategoriesId = categoriesId;
            PrepaymentAvailable = prepaymentAvailable;
        }

        public string SiteUrl { get; }
        public List<string> SocialMedias { get; }
        public List<string> PhotoUris { get; }
        public List<int> CategoriesId { get; }
        public bool PrepaymentAvailable { get; }

        public bool IsValid => !SocialMedias.IsNullOrEmpty() || 
            CategoriesId.IsNullOrEmpty();
    }
}
