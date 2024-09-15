using BuildingBlocks.Domain;

namespace Identity.Domain.Users
{
    public class Address : ValueObject
    {
        public Address(string city, string street, Coords coords)
        {
            City = city;
            Street = street;
            Coords = coords;
        }

        private Address()
        {

        }

        public string City { get; }

        public string Street { get; }

        public Coords Coords { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return Street;
        }
    }
    
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