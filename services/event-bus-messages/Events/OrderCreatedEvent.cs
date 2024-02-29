
namespace Choice.EventBus.Messages.Events
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public OrderCreatedEvent(int orderId, int orderRequestId, string senderGuid, string receiverId, double price,
            double prepayment, int deadline, bool isEnrolled, DateTime enrollmentTime, string status)
        {
            OrderId = orderId;
            OrderRequestId = orderRequestId;
            SenderGuid = senderGuid;
            ReceiverId = receiverId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            IsEnrolled = isEnrolled;
            EnrollmentTime = enrollmentTime;
            Status = status;
        }

        public int OrderId { get; }
        public int OrderRequestId { get; }
        public string SenderGuid { get; } 
        public string ReceiverId { get; } 
        public double Price { get; }
        public double Prepayment { get; }
        public int Deadline { get; }
        public bool IsEnrolled { get; } = false;
        public DateTime EnrollmentTime { get; }
        public string Status { get; }
    }
}
