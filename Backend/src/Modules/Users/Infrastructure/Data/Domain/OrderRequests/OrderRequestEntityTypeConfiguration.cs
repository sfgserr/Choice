using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Categories;
using Users.Domain.OrderRequests;
using Users.Domain.Users.Clients;

namespace Users.Infrastructure.Data.Domain.OrderRequests
{
    internal class OrderRequestEntityTypeConfiguration : IEntityTypeConfiguration<OrderRequest>
    {
        public void Configure(EntityTypeBuilder<OrderRequest> builder)
        {
            builder.ToTable("OrderRequests", "users");

            builder.HasKey(x => x.Id);

            builder.Property<ClientId>("ClientCreatedId").HasColumnName("ClientCreatedId");
            builder.Property<bool>("ToKnowPrice").HasColumnName("ToKnowPrice");
            builder.Property<bool>("ToKnowDeadline").HasColumnName("ToKnowDeadline");
            builder.Property<bool>("ToKnowEnrollmentDate").HasColumnName("ToKnowEnrollmentDate");
            builder.Property<int>("_distance").HasColumnName("Distance");
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<bool>("_isEnrolled").HasColumnName("IsEnrolled");
            builder.Property<DateTime>("_creationDate").HasColumnName("CreationDate");
            builder.Property<CategoryId>("_categoryId")
                .HasConversion(i => i.Value, i => new(i))
                .HasColumnName("CategoryId");
            builder.Property<OrderStatus>("_status")
                .HasConversion(x => x.Value, x => OrderStatus.Parse(x))
                .HasColumnName("Status");
            builder.Property<List<string>>("_photoUris").HasColumnName("PhotoUris");
        }
    }
}
