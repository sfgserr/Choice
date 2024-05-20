
namespace Choice.EventBus.Messages.Events
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public OrderCreatedEvent(int orderId, int orderRequestId, string senderGuid, string receiverId, int price,
            int prepayment, int deadline, DateTime? enrollmentTime)
        {
            OrderId = orderId;
            OrderRequestId = orderRequestId;
            SenderGuid = senderGuid;
            ReceiverId = receiverId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            EnrollmentTime = enrollmentTime;
        }

        public int OrderId { get; }
        public int OrderRequestId { get; }
        public string SenderGuid { get; } 
        public string ReceiverId { get; } 
        public int Price { get; }
        public int Prepayment { get; }
        public int Deadline { get; }
        public bool IsEnrolled { get; } = false;
        public DateTime? EnrollmentTime { get; }
        public int Status { get; } = 1;
    }
}
