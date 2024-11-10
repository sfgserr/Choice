using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Outbox;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages;
using Chat.Infrastructure.Data.Domain.ChatUsers;
using Chat.Infrastructure.Data.Domain.Messages;
using Chat.Infrastructure.Data.Domain.Outbox;
using Chat.Infrastructure.Data.InternalCommands;
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
            builder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            builder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            builder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }

        public DbSet<ChatUser> ChatUsers { get; set; }
        
        public DbSet<Message> Messages { get; set; }
        
        public DbSet<InternalCommand> InternalCommands { get; set; }
        
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}