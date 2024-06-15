using Choice.Common.ValueObjects;
using Choice.CompanyService.Api.Entities;

namespace Choice.CompanyService.Api.ViewModels
{
    public class CompanyViewModel
    {
        public CompanyViewModel(Company company, int distance)
        {
            Guid = company.Guid;
            Title = company.Title;
            IconUri = company.IconUri;
            Address = company.Address;
            AverageGrade = company.AverageGrade;
            ReviewCount = company.ReviewsCount;
            Email = company.Email;
            PhoneNumber = company.PhoneNumber;
            CategoriesId = [.. company.CategoriesId];
            SocialMedias = [.. company.SocialMedias];
            PhotoUris = [.. company.PhotoUris];
            Distance = distance;
        }

        public string Guid { get; }
        public string Title { get; }
        public string IconUri { get; }
        public Address Address { get; }
        public double AverageGrade { get; }
        public int ReviewCount { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public List<int> CategoriesId { get; }
        public List<string> SocialMedias { get; }
        public List<string> PhotoUris { get; }
        public int Distance { get; }
    }
}
