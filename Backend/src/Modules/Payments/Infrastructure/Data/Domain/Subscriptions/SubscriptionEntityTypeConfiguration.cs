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
            builder.Property(e => e.SubscriberId).HasColumnName("SubscriberId");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate");
            builder.Property(e => e.Status).HasConversion(e => e.Value, e => SubscriptionStatus.Parse(e));

            builder.OwnsOne(e => e.Period, b =>
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
