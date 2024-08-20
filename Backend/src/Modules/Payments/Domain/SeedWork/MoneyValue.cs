using BuildingBlocks.Domain;

namespace Payments.Domain.SeedWork
{
    public class MoneyValue : ValueObject
    {
        public MoneyValue(double cost, string currency)
        {
            Cost = cost;
            Currency = currency;
        }

        public double Cost { get; }

        public string Currency { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Cost;
            yield return Currency;
        }
    }
}
