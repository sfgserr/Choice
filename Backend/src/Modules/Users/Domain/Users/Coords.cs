using BuildingBlocks.Domain;

namespace Users.Domain.Users
{
    public class Coords : ValueObject
    {
        public Coords(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Latitude { get; }

        public string Longitude { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
