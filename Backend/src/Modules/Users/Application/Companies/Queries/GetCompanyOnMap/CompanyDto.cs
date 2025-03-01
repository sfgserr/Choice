
namespace Users.Application.Companies.Queries.GetCompanyOnMap
{
    public class CompanyDto
    {
        public Guid Id { get; }

        public string Name { get; }

	    public string Email { get; }

	    public string PhoneNumber { get; }
        
        public string IconUri { get; }
        
        public string Street { get; }

        public string City { get; }

	    public string Description { get;  }
        
        public string[] PhotoUris { get; }

        public IEnumerable<SocialMediaDto> SocialMedias { get; set; }

        public int ReviewsCount { get; }
        
        public double AverageGrade { get;  }

        public string Latitude { get; }
        
        public string Longitude { get; }

        public int Distance { get; set; }
    }
}
