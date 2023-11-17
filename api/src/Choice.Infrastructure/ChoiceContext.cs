using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure
{
    public class ChoiceContext : DbContext
    {
        public ChoiceContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                        .Property(c => c.PhotoUris)
                        .HasConversion(c => string.Join("|", c),
                                       c => c.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Order>()
                        .Property(c => c.PhotoUris)
                        .HasConversion(c => string.Join("|", c),
                                       c => c.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<OrderMessage>()
                        .Property(c => c.PhotoUris)
                        .HasConversion(c => string.Join("|", c),
                                       c => c.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<Review>()
                        .Property(c => c.PhotoUris)
                        .HasConversion(c => string.Join("|", c),
                                       c => c.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMessage> OrderMessages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
    }
}
