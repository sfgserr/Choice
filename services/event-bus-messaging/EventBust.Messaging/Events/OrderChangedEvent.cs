
namespace Choice.EventBust.Messages.Events
{
    public class OrderChangedEvent : IntegrationEvent
    {
        public OrderChangedEvent(int orderId, string receiverId, string type)
        {
            OrderId = orderId;
            ReceiverId = receiverId;
            Type = type;
        }

        public int OrderId { get; }
        public string ReceiverId { get; }
        public string Type { get; }
    }
}
