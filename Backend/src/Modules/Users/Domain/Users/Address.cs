using BuildingBlocks.Domain;

namespace Users.Domain.Users
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
}
