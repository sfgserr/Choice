using BuildingBlocks.Domain;

namespace Payments.Domain.Subscriptions
{
    public class SubscriptionStatus : ValueObject
    {
        private SubscriptionStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static SubscriptionStatus Active { get; } = new SubscriptionStatus("Active");

        public static SubscriptionStatus Expired { get; } = new SubscriptionStatus("Expired");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static SubscriptionStatus Parse(string e)
        {
            return e switch
            {
                "Active" => Active,
                "Expired" => Expired,
                _ => throw new ArgumentException()
            };
        }
    }
}
