
namespace Choice.EventBus.Messages.Events
{
    public class UserEnrolledEvent
    {
        public UserEnrolledEvent(int orderId, string companyId)
        {
            OrderId = orderId;
            CompanyId = companyId;
        }

        public int OrderId { get; }
        public string CompanyId { get; }
    }
}
