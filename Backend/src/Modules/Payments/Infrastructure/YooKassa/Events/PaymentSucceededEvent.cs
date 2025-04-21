using Payments.Infrastructure.YooKassa.Events.Core;

namespace Payments.Infrastructure.YooKassa.Events
{
    public class PaymentSucceededEvent : IYooKassaEvent
    {
        public PaymentSucceededEvent(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Guid PaymentId { get; }
    }
}