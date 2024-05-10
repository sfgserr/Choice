
namespace Choice.EventBus.Messages.Events
{
    public class UserEnrolledEvent
    {
        public UserEnrolledEvent(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
