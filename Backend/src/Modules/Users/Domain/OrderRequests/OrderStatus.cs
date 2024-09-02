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

        public static OrderStatus Parse(string status) => status switch
        {
            "Active" => Active,
            "Finished" => Finished,
            "Cancelled" => Cancelled,
            _ => throw new ArgumentException("")
        };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
