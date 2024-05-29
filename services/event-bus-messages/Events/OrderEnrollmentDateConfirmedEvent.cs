
namespace Choice.EventBus.Messages.Events
{
    public class OrderEnrollmentDateConfirmedEvent
    {
        public OrderEnrollmentDateConfirmedEvent(int orderId, string clientId)
        {
            OrderId = orderId;
            ClientId = clientId;
        }

        public int OrderId { get; }
        public string ClientId { get; }
    }
}
