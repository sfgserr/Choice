using Payments.Infrastructure.YooKassa.Events.Core;

namespace Payments.Infrastructure.YooKassa.Events
{
    public class PayoutSucceededEvent : IYooKassaEvent
    {
        public PayoutSucceededEvent(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}