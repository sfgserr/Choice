using Choice.Chat.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Choice.Chat.Api.Infrastructure.Data
{
    public class ChatDdContext : DbContext
    {
        public ChatDdContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                        .HasKey(m => m.Id);

            builder.Entity<User>()
                   .HasKey(u => u.Guid);

            builder.Entity<Message>()
                        .HasOne(m => m.Receiver)
                        .WithMany()
                        .HasForeignKey(m => m.ReceiverId);

            SeedData.Seed(builder);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
