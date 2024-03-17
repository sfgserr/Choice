using Choice.CompanyService.Api.Entities;
using Choice.CompanyService.Api.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Choice.CompanyService.Api.Infrastructure.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyConfiguration());
        }

        public DbSet<Company> Companies { get; set; }
    }
}
