using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Users;
using Users.Infrastructure.Data.ValueConversion;

namespace Users.Infrastructure.Data.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "users");

            builder.HasKey(u => u.Id);
            
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_phoneNumber").HasColumnName("PhoneNumber");
            builder.Property<string>("_iconUri").HasColumnName("IconUri");
            builder.Property<int>("_reviewsCount").HasColumnName("ReviewsCount");
            builder.Property<double>("_averageGrade").HasColumnName("AverageGrade");
            builder.Property<bool>("IsDataFilled").HasColumnName("IsDataFilled");
            builder.Property<UserRole>("_role")
                .HasConversion(new UserRoleToStringValueConverter())
                .HasColumnName("Role");
            
            builder.OwnsOne<Address>("_address", b =>
            {
                b.Property(x => x.Street).HasColumnName("Street");
                b.Property(x => x.City).HasColumnName("City");

                b.OwnsOne(x => x.Coords, b =>
                {
                    b.Property(x => x.Latitude).HasColumnName("Latitude");
                    b.Property(x => x.Longitude).HasColumnName("Longitude");
                });
            });
        }
    }
}