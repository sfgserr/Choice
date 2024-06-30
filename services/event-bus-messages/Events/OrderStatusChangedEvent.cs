
namespace Choice.EventBus.Messages.Events
{
    public class OrderStatusChangedEvent
    {
        public OrderStatusChangedEvent(int orderRequestId, int orderId, int orderStatus, string receiverId)
        {
            OrderRequestId = orderRequestId;
            OrderId = orderId;
            OrderStatus = orderStatus;
            ReceiverId = receiverId;
        }

        public int OrderRequestId { get; }
        public int OrderId { get; }
        public int OrderStatus { get; }
        public string ReceiverId { get; }
    }
}
