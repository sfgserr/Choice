using Microsoft.EntityFrameworkCore;
using Payments.Domain.EnrollmentPayments;
using Payments.Domain.Subscriptions;
using Payments.Domain.SubscritpionPayments;

namespace Payments.Application.Contracts
{
    public interface IPaymentsDbContext
    {
        DbSet<SubscriptionPayment> SubscriptionPayments { get; }

        DbSet<Subscription> Subscriptions { get; }
        
        DbSet<EnrollmentPayment> EnrollmentPayments { get; }
    }
}