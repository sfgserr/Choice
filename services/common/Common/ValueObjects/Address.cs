using Choice.Common.SeedWork;

namespace Choice.Common.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public string Street { get; private set; }
        public string City { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
        }
    }
}
