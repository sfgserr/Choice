using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Infrastructure.Data
{
    public class ChatDdContext : DbContext
    {
        public ChatDdContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                   .HasKey(m => m.Id);

            builder.Entity<Message>()
                   .Ignore(m => m.Content);

            builder.Entity<User>()
                   .HasKey(u => u.Guid);

            SeedData.Seed(builder);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
