using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.Domain.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "identity");

            builder.HasKey(x => x.Id);
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_password").HasColumnName("HashedPassword");
            builder.Property<string>("_phoneNumber").HasColumnName("PhoneNumber");
            builder.Property<bool>("_isSubscribed").HasColumnName("IsSubscribed");
            builder.Property<UserRole>("_role")
                .HasConversion(x => x.Value, x => UserRole.Parse(x))
                .HasColumnName("Role");
            
            builder.OwnsOne<Address>("_address", b =>
            {
                b.Property(x => x.City).HasColumnName("City");
                b.Property(x => x.Street).HasColumnName("Street");

                b.OwnsOne(x => x.Coords, b =>
                {
                    b.Property(x => x.Latitude).HasColumnName("Latitude");
                    b.Property(x => x.Longitude).HasColumnName("Longitude");
                });
            });
        }
    }
}