using Payments.Domain.Payers;

namespace Payments.Domain.SubscriptionPayments
{
    public interface ISubscriptionPaymentsCounter
    {
        int Count(PayerId payerId);
    }
}