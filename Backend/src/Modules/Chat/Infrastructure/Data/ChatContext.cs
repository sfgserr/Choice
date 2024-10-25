using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages;
using Chat.Infrastructure.Data.Domain.ChatUsers;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data
{
    public class ChatContext : DbContext, IChatDbContext
    {
        public ChatContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ChatUserEntityTypeConfiguration());
        }

        public DbSet<ChatUser> ChatUsers { get; set; }
        
        public DbSet<Message> Messages { get; set; }
    }
}