using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.OrderRequests;
using Users.Domain.OrderResponses;

namespace Users.Infrastructure.Data.Domain.OrderResponses
{
    internal class OrderResponseEntityTypeConfiguration : IEntityTypeConfiguration<OrderResponse>
    {
        public void Configure(EntityTypeBuilder<OrderResponse> builder)
        {
            builder.ToTable("OrderResponses", "users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Prepayment).HasColumnName("Prepayment");
            builder.Property(x => x.Price).HasColumnName("Price");
            builder.Property(x => x.ClientId).HasColumnName("ClientId");
            builder.Property(x => x.CompanyId).HasColumnName("CompanyId");
            builder.Property(x => x.Deadline).HasColumnName("Deadline");
            builder.Property(x => x.Status)
                .HasConversion(x => x.Value, x => OrderStatus.Parse(x));

            builder.OwnsMany(x => x.Reviews, b =>
            {
                b.ToTable("Reviews", "users");

                b.HasKey(x => new { x.ResponseId, x.AuthorId, x.ToUserId });

                b.Property(x => x.Text).HasColumnName("Text");

                b.Property(x => x.Grade).HasColumnName("Grade");
            });
        }
    }
}
