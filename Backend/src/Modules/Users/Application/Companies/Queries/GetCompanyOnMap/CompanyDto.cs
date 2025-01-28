
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Queries.GetCompanyOnMap
{
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string IconUri { get; set; }
        
        public string Street { get; set; }

        public string City { get; set; }

	    public string Description { get; set; }
        
        public List<string> PhotoUris { get; set; }

        public List<SocialMedia> SocialMedias { get; set; }

        public int ReviewsCount { get; set; }
        
        public double AverageGrade { get; set; }

        public int Distance { get; set; }
    }
}
