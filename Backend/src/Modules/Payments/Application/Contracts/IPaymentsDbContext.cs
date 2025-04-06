using Microsoft.EntityFrameworkCore;
using Payments.Domain.SubscriptionPayments;
using Payments.Domain.Subscriptions;
using Payments.Domain.Wallets;

namespace Payments.Application.Contracts
{
    public interface IPaymentsDbContext
    {
        DbSet<SubscriptionPayment> SubscriptionPayments { get; }

        DbSet<Subscription> Subscriptions { get; }
        
        DbSet<Wallet> Wallets { get; }
    }
}