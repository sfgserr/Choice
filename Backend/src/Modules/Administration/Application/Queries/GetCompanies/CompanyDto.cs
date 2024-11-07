namespace Administration.Application.Queries.GetCompanies
{
    public class CompanyDto
    {
        public CompanyDto(
            Guid id, 
            string iconUri,  
            string name, 
            string city, 
            string street)
        {
            Id = id;
            IconUri = iconUri;
            Name = name;
            City = city;
            Street = street;
        }

        public Guid Id { get; }

        public string IconUri { get; }
        
        public string Name { get; }

        public string City { get; }
        
        public string Street { get; }
    }
}