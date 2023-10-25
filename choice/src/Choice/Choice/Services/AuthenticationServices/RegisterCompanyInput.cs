using Choice.Domain.Models;
using System.Collections.Generic;

namespace Choice.Services.AuthenticationServices
{
    public class RegisterCompanyInput
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordConfirmtion { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string SiteUri { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<SocialMedia> SocialMedias { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<string> PhotoUris { get; set; } = new List<string>();
        public PrepaymentAvailability PrepaymentAvailability { get; set; }
    }
}
