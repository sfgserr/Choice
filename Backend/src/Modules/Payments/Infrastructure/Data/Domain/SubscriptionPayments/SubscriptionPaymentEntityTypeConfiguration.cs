using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.SeedWork;
using Payments.Domain.SubscriptionPayments;

namespace Payments.Infrastructure.Data.Domain.SubscriptionPayments
{
    internal class SubscriptionPaymentEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionPayment>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPayment> builder)
        {
            builder.ToTable("SubscriptionPayments", "payments");

            builder.HasKey(x => x.Id);

            builder.HasKey(s => s.Id);
            builder.Property(x => x.PayerId).HasColumnName("PayerId");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate");
            builder.Property(e => e.Status).HasConversion(e => e.Value, e => PaymentStatus.Parse(e));

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
