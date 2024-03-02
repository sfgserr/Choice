using Choice.ClientService.Domain.Common;

namespace Choice.ClientService.Domain.ClientAggregate
{
    public class Address : ValueObject
    {
        public Address(string street, string city, string state, string country)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
        }
    }
}
