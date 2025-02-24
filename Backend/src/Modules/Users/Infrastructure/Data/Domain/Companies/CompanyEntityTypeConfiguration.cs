using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Infrastructure.Data.Domain.Companies
{
    internal class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", "users");

            builder.HasKey(x => x.Id);
            
            builder.Property<UserId>("_userId").HasColumnName("UserId");
            
            builder.HasOne<User>("_user")
                .WithOne()
                .HasForeignKey<Company>("_userId");

            builder.Navigation("_user").AutoInclude();

            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<bool>("_isPrepaymentAvailable").HasColumnName("IsPrepaymentAvailable");
            
            builder.Property<List<string>>("_photoUris").HasColumnName("PhotoUris");
            builder.Property<List<int>>("_categories").HasColumnName("CategoriesId");

            builder.OwnsMany<SocialMedia>("_socialMedias", y =>
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
