
namespace Choice.EventBus.Messages.Events
{
    public class OrderEnrollmentDateChangedEvent
    {
        public OrderEnrollmentDateChangedEvent(int orderId, DateTime enrollmentDate, string receiverId)
        {
            OrderId = orderId;
            EnrollmentDate = enrollmentDate;
            ReceiverId = receiverId;
        }

        public int OrderId { get; }
        public DateTime EnrollmentDate { get; }
        public string ReceiverId { get; }
    }
}
