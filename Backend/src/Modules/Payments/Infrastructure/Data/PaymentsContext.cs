using Microsoft.EntityFrameworkCore;
using Payments.Application.Contracts;
using Payments.Domain.EnrollmentPayments;
using Payments.Domain.Subscriptions;
using Payments.Domain.SubscritpionPayments;
using Payments.Infrastructure.Data.Domain.EnrollmentPayments;
using Payments.Infrastructure.Data.Domain.SubscriptionPayments;

namespace Payments.Infrastructure.Data
{
    internal class PaymentsContext : DbContext, IPaymentsDbContext
    {
        public PaymentsContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EnrollmentPaymentEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubscriptionEntityTypeConfiguration());
        }

        public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<EnrollmentPayment> EnrollmentPayments { get; set; }
    }
}
