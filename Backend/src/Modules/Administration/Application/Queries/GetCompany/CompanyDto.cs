namespace Administration.Application.Queries.GetCompany
{
    public class CompanyDto
    {
        public Guid Id { get; }

        public string IconUri { get; }

        public double AverageGrade { get; }
        
        public string Name { get; }
        
        public string Description { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }
        
        public string Street { get; }

        public IEnumerable<SocialMediaDto> SocialMedias { get; set; }

        public int[] CategoryIds { get; }

        public string[] PhotoUris { get; }

        public bool IsPrepaymentAvailable { get; }
    }
}