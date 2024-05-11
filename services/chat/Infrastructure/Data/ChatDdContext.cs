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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                        .HasKey(m => m.Id);

            modelBuilder.Entity<Message>()
                        .HasOne(m => m.Receiver)
                        .WithMany()
                        .HasForeignKey(m => m.ReceiverId);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
