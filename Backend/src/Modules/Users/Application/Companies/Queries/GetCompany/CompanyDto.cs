
namespace Users.Application.Companies.Queries.GetCompany
{
    public class CompanyDto
    {
        public Guid Id { get; }
        
        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Description { get; }

        public string[] PhotoUris { get; }

        public int[] Categories { get; }
        
        public IEnumerable<SocialMediaDto> SocialMedias { get; set; }

        public string City { get; }

        public string Street { get; }
        
        public bool IsPrepaymentAvailable { get; }
    }
}