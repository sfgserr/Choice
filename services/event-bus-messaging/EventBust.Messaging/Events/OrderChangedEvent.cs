
namespace Choice.EventBust.Messages.Events
{
    public class OrderChangedEvent : IntegrationEvent
    {
        public OrderChangedEvent(int orderId, string receiverId)
        {
            OrderId = orderId;
            ReceiverId = receiverId;
        }

        public int OrderId { get; }
        public string ReceiverId { get; }
    }
}
