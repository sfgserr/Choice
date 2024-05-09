
namespace Choice.EventBus.Messages.Events
{
    public class UserEnrolledEvent
    {
        public UserEnrolledEvent(int orderId, string receiverId)
        {
            OrderId = orderId;
            ReceiverId = receiverId;
        }

        public int OrderId { get; }
        public string ReceiverId { get; }
    }
}
