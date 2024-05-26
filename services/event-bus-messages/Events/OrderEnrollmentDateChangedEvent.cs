
namespace Choice.EventBus.Messages.Events
{
    public class OrderEnrollmentDateChangedEvent
    {
        public OrderEnrollmentDateChangedEvent(int orderId, DateTime? enrollmentDate, string receiverId, 
            bool isClientChanged)
        {
            OrderId = orderId;
            EnrollmentDate = enrollmentDate;
            ReceiverId = receiverId;
            IsClientChanged = isClientChanged;
        }

        public int OrderId { get; }
        public DateTime? EnrollmentDate { get; }
        public string ReceiverId { get; }
        public bool IsClientChanged { get; }
    }
}
