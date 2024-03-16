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
            SiteUrl = company.SiteUrl;
            Address = company.Address;
            AverageGrade = company.AverageGrade;
            SocialMedias = company.SocialMedias.ToList();
            PhotoUris = company.PhotoUris.ToList();
            CategoriesId = company.CategoriesId.ToList();
        }

        public int Id { get; }
        public string Guid { get; }
        public string Title { get; }
        public string PhoneNumber { get; }
        public string SiteUrl { get; }
        public Address Address { get; }
        public double AverageGrade { get; }
        public List<string> SocialMedias { get; }
        public List<string> PhotoUris { get; }
        public List<int> CategoriesId { get; }
    }
}
