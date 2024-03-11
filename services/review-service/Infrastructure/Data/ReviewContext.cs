using Microsoft.EntityFrameworkCore;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Data.Configurations;

namespace Choice.ReviewService.Api.Infrastructure.Data
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
