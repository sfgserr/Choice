using Payments.Infrastructure.YooKassa.Events.Core;

namespace Payments.Infrastructure.YooKassa.Events
{
    public class PaymentSucceededEvent : IYooKassaEvent
    {
        public PaymentSucceededEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}