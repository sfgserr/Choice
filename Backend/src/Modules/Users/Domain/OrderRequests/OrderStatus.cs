using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests
{
    public class OrderStatus : ValueObject
    {
        private OrderStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static OrderStatus Active { get; } = new OrderStatus("Active");

        public static OrderStatus Finished { get; } = new OrderStatus("Finished");

        public static OrderStatus Cancelled { get; } = new OrderStatus("Cancelled");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
