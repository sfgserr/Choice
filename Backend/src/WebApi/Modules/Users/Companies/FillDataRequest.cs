namespace WebApi.Modules.Users.Companies
{
    public class FillDataRequest
    {
        public FillDataRequest(
            string description, 
            List<int> categoryIds, 
            List<string> photoUris,
            List<string> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            Description = description;
            CategoryIds = categoryIds;
            PhotoUris = photoUris;
            IsPrepaymentAvailable = isPrepaymentAvailable;
            SocialMediaUris = socialMediaUris;
        }

        public string Description { get; }

        public List<int> CategoryIds { get; }

        public List<string> PhotoUris { get; }
        
        public List<string> SocialMediaUris { get; }
        
        public bool IsPrepaymentAvailable { get; }
    }
}