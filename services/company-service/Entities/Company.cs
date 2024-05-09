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
        public string IconUri { get; private set; } = "defaulturi";
        public Address Address { get; private set; }    
        public double AverageGrade { get; private set; } = 0;
        public int ReviewsCount { get; private set; } = 0;
        public List<string> SocialMedias { get; private set; } = [];
        public List<string> PhotoUris { get; private set; } = [];
        public List<int> CategoriesId { get; private set; } = [];
        public bool PrepaymentAvailable { get; private set; } = false;
        public bool IsDataFilled { get; private set; } = false;

        public void FillCompanyData(string siteUrl, List<string> socialMedias, List<string> photoUris, 
            List<int> categoriesId, bool prepaymentAvailable)
        {
            SiteUrl = siteUrl;

            SocialMedias = socialMedias;
            PhotoUris = photoUris;
            CategoriesId = categoriesId;
            PrepaymentAvailable = prepaymentAvailable;

            IsDataFilled = true;
        }

        public void ChangeData(string title, string phoneNumber, string email, string siteUrl, string city, 
            string street, List<string> socialMedias, List<string> photoUris, List<int> categoriesId)
        {
            Title = title;
            PhoneNumber = phoneNumber;
            Email = email;
            SiteUrl = siteUrl;
            Address = new(street, city);
            SocialMedias = socialMedias;
            PhotoUris = photoUris;
            CategoriesId = categoriesId;
        }

        public void ChangeIconUri(string iconUri)
        {
            IconUri = iconUri;
        }

        public void AddReview(int grade)
        {
            AverageGrade = ReviewsCount < 1 ? grade : (ReviewsCount * AverageGrade + grade) / (ReviewsCount + 1);
            ReviewsCount++;
        }
    }
}
