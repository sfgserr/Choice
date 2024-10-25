using BuildingBlocks.Infrastructure.InternalCommands;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages;
using Chat.Infrastructure.Data.Domain.ChatUsers;
using Chat.Infrastructure.Data.Domain.InternalCommands;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data
{
    public class ChatContext : DbContext, IChatDbContext
    {
        public ChatContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ChatUserEntityTypeConfiguration());
            builder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }

        public DbSet<ChatUser> ChatUsers { get; set; }
        
        public DbSet<Message> Messages { get; set; }
        
        public DbSet<InternalCommand> InternalCommands { get; set; }
    }
}