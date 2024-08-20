using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.OrderMessages
{
    public class OrderMessageStatus : ValueObject
    {
        private OrderMessageStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static OrderMessageStatus Active { get; } = new("Active");

        public static OrderMessageStatus Cancelled { get; } = new("Cancelled");

        public static OrderMessageStatus Finished { get; } = new("Finished");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
