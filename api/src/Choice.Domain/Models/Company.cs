
namespace Choice.Domain.Models
{
    public enum PrepaymentAvailability
    {
        With = 0,
        Without = 1
    }

    public class Company : User
    {
        public string Title { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string SiteUri { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
        public List<string> PhotoUris { get; set; } = new List<string>();
        public PrepaymentAvailability PrepaymentAvailability { get; set; }
        public bool ShowOnMap { get; set; } = true;
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
