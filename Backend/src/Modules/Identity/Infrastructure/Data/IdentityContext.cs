using BuildingBlocks.Infrastructure.InternalCommands;
using Identity.Application.Contracts;
using Identity.Domain.Users;
using Identity.Infrastructure.Data.Domain.Users;
using Identity.Infrastructure.Data.InternalCommands;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data
{
    public class IdentityContext : DbContext, IIdentityDbContext
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}