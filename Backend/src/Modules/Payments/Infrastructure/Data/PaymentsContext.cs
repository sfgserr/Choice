using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Payments.Application.Contracts;
using Payments.Domain.EnrollmentPayments;
using Payments.Domain.SubscriptionPayments;
using Payments.Domain.Subscriptions;
using Payments.Infrastructure.Data.Domain.EnrollmentPayments;
using Payments.Infrastructure.Data.Domain.SubscriptionPayments;
using Payments.Infrastructure.Data.Domain.Subscriptions;
using Payments.Infrastructure.Data.InternalCommands;
using Payments.Infrastructure.Data.Outbox;

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
            builder.ApplyConfiguration(new SubscriptionPaymentEntityTypeConfiguration());
            builder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            builder.ApplyConfiguration(new OutboxEntityTypeConfiguration());
        }

        public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<EnrollmentPayment> EnrollmentPayments { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
