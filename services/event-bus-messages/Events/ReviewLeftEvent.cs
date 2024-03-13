
namespace Choice.EventBus.Messages.Events
{
    public class ReviewLeftEvent : IntegrationEvent
    {
        public ReviewLeftEvent(int orderId, string userGuid, int grade)
        {
            OrderId = orderId;
            UserGuid = userGuid;
            Grade = grade;
        }

        public int OrderId { get; }
        public string UserGuid { get; }
        public int Grade { get; }
    }
}
