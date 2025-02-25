using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Subscriptions;

namespace Payments.Infrastructure.Data.Domain.Subscriptions
{
    internal class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions", "payments");

            builder.HasKey(s => s.Id);
            builder.Property<SubscriberId>("_subscriberId").HasColumnName("SubscriberId");
            builder.Property<DateTime>("_expirationDate").HasColumnName("ExpirationDate");
            builder.Property<SubscriptionStatus>("_status")
                .HasConversion(e => e.Value, e => SubscriptionStatus.Parse(e))
                .HasColumnName("SubscriptionStatus");

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
