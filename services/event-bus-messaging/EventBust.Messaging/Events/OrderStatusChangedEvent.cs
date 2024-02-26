
namespace Choice.EventBust.Messages.Events
{
    public class OrderStatusChangedEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
    }
}
