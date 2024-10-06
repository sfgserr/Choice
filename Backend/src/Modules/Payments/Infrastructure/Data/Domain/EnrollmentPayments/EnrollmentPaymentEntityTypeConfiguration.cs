using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.EnrollmentPayments;
using Payments.Domain.SeedWork;

namespace Payments.Infrastructure.Data.Domain.EnrollmentPayments
{
    internal class EnrollmentPaymentEntityTypeConfiguration : IEntityTypeConfiguration<EnrollmentPayment>
    {
        public void Configure(EntityTypeBuilder<EnrollmentPayment> builder)
        {
            builder.ToTable("EnrollmentTables", "payments");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ResponseId).HasColumnName("ResponseId");
            builder.Property(e => e.PayerId).HasColumnName("PayerId");
            builder.Property(e => e.Status).HasConversion(e => e.Value, e => PaymentStatus.Parse(e));

            builder.OwnsOne(e => e.Cost, b =>
            {
                b.Property(x => x.Currency).HasColumnName("Currency");
                b.Property(x => x.Cost).HasColumnName("Cost");
            });
        }
    }
}
