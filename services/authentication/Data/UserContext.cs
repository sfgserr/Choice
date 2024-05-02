using Choice.Authentication.Api.EntityConfigurations;
using Choice.Authentication.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Choice.Authentication.Api.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
