using Choice.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Choice.CompanyService.Api.Entities
{
    public class Company
    {
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
        public string Guid { get; private set; }
        public string Title { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string SiteUrl { get; private set; } = string.Empty;
        public Address Address { get; private set; }    
        public double AverageGrade { get; private set; } = 0;
        public int ReviewCount { get; private set; } = 0;
        public List<string> SocialMedias { get; private set; } = new();
        public List<string> PhotoUris { get; private set; } = new();
        public List<int> CategoriesId { get; private set; } = new();

        public void FillCompanyData(string siteUrl, string street, string city,
            List<string> socialMedias, List<string> photoUris, List<int> categoriesId)
        {
            SiteUrl = siteUrl;
            Address = new(street, city);

            SocialMedias.AddRange(socialMedias);
            PhotoUris.AddRange(photoUris);
            CategoriesId.AddRange(categoriesId);
        }

        public void AddReview(int grade)
        {
            AverageGrade = ReviewCount < 1 ? grade : (ReviewCount * AverageGrade + grade) / (ReviewCount + 1);
            ReviewCount++;
        }
    }
}
