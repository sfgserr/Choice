using Payments.Domain.Payers;

namespace Payments.Domain.SubscritpionPayments
{
    public interface ISubscriptionPaymentsCounter
    {
        int Count(PayerId payerId);
    }
}