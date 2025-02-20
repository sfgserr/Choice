using Microsoft.EntityFrameworkCore;
using Payments.Domain.SubscriptionPayments;
using Payments.Domain.Subscriptions;

namespace Payments.Application.Contracts
{
    public interface IPaymentsDbContext
    {
        DbSet<SubscriptionPayment> SubscriptionPayments { get; }

        DbSet<Subscription> Subscriptions { get; }
    }
}