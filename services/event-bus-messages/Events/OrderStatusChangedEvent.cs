
namespace Choice.EventBus.Messages.Events
{
    public class OrderStatusChangedEvent
    {
        public OrderStatusChangedEvent(int orderRequestId, int orderStatus, string receiverId)
        {
            OrderRequestId = orderRequestId;
            OrderStatus = orderStatus;
            ReceiverId = receiverId;
        }

        public int OrderRequestId { get; }
        public int OrderStatus { get; }
        public string ReceiverId { get; }
    }
}
