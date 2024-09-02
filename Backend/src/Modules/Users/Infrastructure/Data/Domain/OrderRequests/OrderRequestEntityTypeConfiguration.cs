using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.OrderRequests;

namespace Users.Infrastructure.Data.Domain.OrderRequests
{
    internal class OrderRequestEntityTypeConfiguration : IEntityTypeConfiguration<OrderRequest>
    {
        public void Configure(EntityTypeBuilder<OrderRequest> builder)
        {
            builder.ToTable("OrderRequests", "users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientCreatedId).HasColumnName("ClientCreatedId");
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate");
            builder.Property(x => x.CategoryId)
                .HasConversion(i => i.Value, i => new(i));
            builder.Property(x => x.Status)
                .HasConversion(x => x.Value, x => OrderStatus.Parse(x));
        }
    }
}
