using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Users.Companies;

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
            builder.Property<List<int>>("_categories").HasColumnName("CategoriesId");

            builder.OwnsMany(x => x.SocialMedias, y =>
            {
                y.Property<CompanyId>("CompanyId")
                    .HasColumnType("uuid");

                y.Property(z => z.Url).HasColumnName("Url");
                y.Property(z => z.Platform).HasColumnName("Platform");

                y.HasKey("CompanyId", "Platform");

                y.ToTable("SocialMedias", "users");

                y.WithOwner()
                    .HasForeignKey("CompanyId");
            });
        }
    }
}
