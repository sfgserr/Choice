using Administration.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Infrastructure.Data.Domain.Categories
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "administration");

            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new(x))
                .ValueGeneratedOnAdd()
                .HasValueGenerator<CategoryIdValueGenerator>();
            
            builder.HasKey(x => x.Id);
            
            builder.Property<string>("_title").HasColumnName("Title");
            builder.Property<string>("_iconUri").HasColumnName("IconUri");
        }
    }
}