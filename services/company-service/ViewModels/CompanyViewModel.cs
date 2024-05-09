using Choice.Common.ValueObjects;
using Choice.CompanyService.Api.Entities;

namespace Choice.CompanyService.Api.ViewModels
{
    public class CompanyViewModel
    {
        public CompanyViewModel(Company company, int distance)
        {
            Title = company.Title;
            Address = company.Address;
            AverageGrade = company.AverageGrade;
            ReviewCount = company.ReviewsCount;
            CategoriesId = company.CategoriesId.ToList();
            SocialMedias = company.SocialMedias.ToList();
            Distance = distance;
        }

        public string Title { get; }
        public Address Address { get; }
        public double AverageGrade { get; }
        public int ReviewCount { get; }
        public List<int> CategoriesId { get; }
        public List<string> SocialMedias { get; }
        public int Distance { get; }
    }
}
