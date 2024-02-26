
namespace Choice.EventBust.Messages.Events
{
    public class OrderEnrollmentDateChangedEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
