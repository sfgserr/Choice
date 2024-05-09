
namespace Choice.EventBus.Messages.Events
{
    public class OrderEnrollmentDateChangedEvent
    {
        public OrderEnrollmentDateChangedEvent(int orderId, DateTime enrollmentDate)
        {
            OrderId = orderId;
            EnrollmentDate = enrollmentDate;
        }

        public int OrderId { get; }
        public DateTime EnrollmentDate { get; }
    }
}
