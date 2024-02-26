
namespace Choice.EventBust.Messages.Events
{
    public class ClientEnrolledEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public bool IsEnrolled { get; set; }    
    }
}
