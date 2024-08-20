using Payments.Domain.Payers;

namespace Payments.Domain.SubscritpionPayments
{
    public interface ISubscriptionsCounter
    {
        int GetSubscriptionCounter(PayerId payerId);
    }
}
