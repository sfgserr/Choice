
namespace Users.Application.Companies.Queries.GetCompanies
{
    public class CompanyDto
    {
        public CompanyDto(Guid id, string iconUri, double averageGrade, string latitude, string longitude)
        {
            Id = id;
            IconUri = iconUri;
            AverageGrade = averageGrade;
            Latitude = latitude;
            Longitude = longitude;
        }
        
        public Guid Id { get; }
        
        public string IconUri { get; }
        
        public double AverageGrade { get; }

        public string Latitude { get; }

        public string Longitude { get; }
    }
}