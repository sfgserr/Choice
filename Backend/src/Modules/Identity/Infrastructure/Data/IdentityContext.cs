using Identity.Application.Contracts;
using Identity.Domain.Users;
using Identity.Infrastructure.Data.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data
{
    public class IdentityContext : DbContext, IIdentityDbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}