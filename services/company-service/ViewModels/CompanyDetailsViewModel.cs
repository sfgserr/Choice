using Choice.Common.ValueObjects;
using Choice.CompanyService.Api.Entities;

namespace Choice.CompanyService.Api.ViewModels
{
    public class CompanyDetailsViewModel
    {
        public CompanyDetailsViewModel(Company company)
        {
            Id = company.Id;
            Guid = company.Guid;
            Title = company.Title;
            PhoneNumber = company.PhoneNumber;
            Email = company.Email;
            IconUri = company.IconUri;
            SiteUrl = company.SiteUrl;
            Address = company.Address;
            Coords = company.Coordinates;
            AverageGrade = company.AverageGrade;
            SocialMedias = company.SocialMedias.ToList();
            PhotoUris = company.PhotoUris.ToList();
            CategoriesId = company.CategoriesId.ToList();
            PrepaymentAvailable = company.PrepaymentAvailable;
            ReviewsCount = company.ReviewsCount;
        }

        public int Id { get; }
        public string Guid { get; }
        public string Title { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string IconUri { get; }
        public string SiteUrl { get; }
        public Address Address { get; }
        public string Coords { get; }
        public double AverageGrade { get; }
        public List<string> SocialMedias { get; }
        public List<string> PhotoUris { get; }
        public List<int> CategoriesId { get; }
        public bool PrepaymentAvailable { get; }
        public int ReviewsCount { get; }
    }
}
