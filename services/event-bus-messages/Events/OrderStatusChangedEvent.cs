
namespace Choice.EventBus.Messages.Events
{
    public class OrderStatusChangedEvent
    {
        public OrderStatusChangedEvent(int orderRequestId, int orderStatus)
        {
            OrderRequestId = orderRequestId;
            OrderStatus = orderStatus;
        }

        public int OrderRequestId { get; }
        public int OrderStatus { get; }
    }
}
