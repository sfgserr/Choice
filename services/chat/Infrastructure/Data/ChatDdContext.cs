using Choice.Chat.Api.Entities;
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
                   .Property(m => m.Body)
                   .HasConversion(b => JsonConvert.SerializeObject(b), j => JsonConvert.DeserializeObject(j));

            builder.Entity<User>()
                   .HasKey(u => u.Guid);

            SeedData.Seed(builder);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
