using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Payers;
using Payments.Domain.SeedWork;
using Payments.Domain.SubscriptionPayments;
using Payments.Domain.Subscriptions;

namespace Payments.Infrastructure.Data.Domain.SubscriptionPayments
{
    internal class SubscriptionPaymentEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionPayment>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPayment> builder)
        {
            builder.ToTable("SubscriptionPayments", "payments");

            builder.HasKey(x => x.Id);
            
            builder.Property<PayerId>("_payerId").HasColumnName("PayerId");
            builder.Property<DateTime>("_expirationDate").HasColumnName("ExpirationDate");
            builder.Property<PaymentStatus>("_status")
                .HasConversion(e => e.Value, e => PaymentStatus.Parse(e))
                .HasColumnName("Status");

            builder.OwnsOne<SubscriptionPeriod>("_period", b =>
            {
                b.Property(x => x.Value).HasColumnName("PeriodName");
                b.OwnsOne(x => x.Cost, b =>
                {
                    b.Property(x => x.Cost).HasColumnName("PeriodCost");
                    b.Property(x => x.Currency).HasColumnName("PeriodCostCurrency");
                });
            });
        }
    }
}
