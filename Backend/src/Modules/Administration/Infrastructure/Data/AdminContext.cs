using Administration.Application.Contracts;
using Administration.Domain.Categories;
using Administration.Infrastructure.Data.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infrastructure.Data
{
    internal class AdminContext : DbContext, IAdministrationDbContext
    {
        public AdminContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        }

        public DbSet<Category> Categories { get; set; }
    }
}