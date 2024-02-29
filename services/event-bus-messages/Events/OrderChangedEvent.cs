
namespace Choice.EventBus.Messages.Events
{
    public class OrderChangedEvent : IntegrationEvent
    {
        public OrderChangedEvent(int orderId, string receiverId, string type, string senderId)
        {
            OrderId = orderId;
            ReceiverId = receiverId;
            Type = type;
            SenderId = senderId;
        }

        public int OrderId { get; }
        public string ReceiverId { get; }
        public string SenderId { get; }
        public string Type { get; }
    }
}
