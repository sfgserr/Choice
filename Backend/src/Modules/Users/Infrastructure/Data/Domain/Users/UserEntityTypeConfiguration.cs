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

            builder.Property<UserRole>("Role")
                .HasConversion(new UserRoleToStringValueConverter());
        }
    }
}
