using BuildingBlocks.Domain;
using Payments.Domain.SeedWork;

namespace Payments.Domain.Subscriptions
{
    public class SubscriptionPeriod : ValueObject
    {
        private SubscriptionPeriod(string value, MoneyValue cost)
        {
            Value = value;
            Cost = cost;
        }

        public string Value { get; }

        public MoneyValue Cost { get; }

        public static SubscriptionPeriod Month = new("Month", new(800, "RUB"));

        public static SubscriptionPeriod HalfYear = new("HalfYear", new(4320, "RUB"));

        public static SubscriptionPeriod Year = new("Year", new(7776, "RUB"));

        public static SubscriptionPeriod Parse(string s) => s switch
        {
            "Month" => Month,
            "HalfYear" => HalfYear,
            "Year" => Year,
            _ => throw new ArgumentException("No such period")
        };
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Cost;
        }
    }
}
