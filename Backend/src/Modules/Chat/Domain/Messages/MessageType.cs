using BuildingBlocks.Domain;

namespace Chat.Domain.Messages
{
    public class MessageType : ValueObject
    {
        private MessageType(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static MessageType Text { get; } = new("Text");

        public static MessageType Image { get; } = new("Image");

        public static MessageType Order { get; } = new("Order");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static MessageType Parse(string type) => type switch
        {
            "Text" => Text,
            "Image" => Image,
            "Order" => Order,
            _ => throw new ArgumentException()
        };
    }
}
