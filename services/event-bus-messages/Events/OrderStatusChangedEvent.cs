
namespace Choice.EventBus.Messages.Events
{
    public class OrderStatusChangedEvent
    {
        public OrderStatusChangedEvent(int orderRequestId, int orderStatus, string orderReceiverId)
        {
            OrderRequestId = orderRequestId;
            OrderStatus = orderStatus;
            OrderReceiverId = orderReceiverId;
        }

        public int OrderRequestId { get; }
        public int OrderStatus { get; }
        public string OrderReceiverId { get; }
    }
}
