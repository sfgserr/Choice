using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Categories;
using Users.Domain.Users.Companies;
using Users.Infrastructure.Data.ValueConversion;

namespace Users.Infrastructure.Data.Domain.Companies
{
    internal class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", "users");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Company>(x => x.UserId);

            builder.Navigation(x => x.User).AutoInclude();

            builder.Property<List<string>>("_photoUris").HasColumnName("PhotoUris");
            builder.Property<List<string>>("_socialMediaUris").HasColumnName("SocialMediaUris");
            builder.Property<List<CategoryId>>("_categories")
                .HasConversion(new CategoryIdCollectionToIntCollectionValueConverter())
                .HasColumnName("CategoriesId");
        }
    }
}
