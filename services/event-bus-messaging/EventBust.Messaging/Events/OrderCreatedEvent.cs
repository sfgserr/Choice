
namespace Choice.EventBust.Messages.Events
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public int OrderRequestId { get; set; }
        public string SenderGuid { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Prepayment { get; set; }
        public int Deadline { get; set; }
        public bool IsEnrolled { get; set; } = false;
        public DateTime EnrollmentTime { get; set; }
        public bool IsCanceled { get; set; } = false;
    }
}
