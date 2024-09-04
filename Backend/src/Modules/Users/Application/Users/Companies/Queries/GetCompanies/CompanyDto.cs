
namespace Users.Application.Users.Companies.Queries.GetCompanies
{
    public class CompanyDto
    {
        public CompanyDto(string iconUri, double averageGrade, string latitude, string longitude)
        {
            IconUri = iconUri;
            AverageGrade = averageGrade;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string IconUri { get; }
        
        public double AverageGrade { get; }

        public string Latitude { get; }

        public string Longitude { get; }
    }
}