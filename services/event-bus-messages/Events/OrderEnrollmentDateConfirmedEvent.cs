
namespace Choice.EventBus.Messages.Events
{
    public class OrderEnrollmentDateConfirmedEvent
    {
        public OrderEnrollmentDateConfirmedEvent(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
