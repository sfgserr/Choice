using Chat.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Data.Domain.Messages
{
    internal class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages", "chat");

            builder.HasKey(x => x.Id);

            builder.Navigation(x => x.OrderMessage).AutoInclude();
            
            builder.Property(x => x.Type).HasColumnName("Type");
            builder.Property(x => x.Body).HasColumnName("Body");
            builder.Property(x => x.FromUserId).HasColumnName("FromUserId");
            builder.Property(x => x.ToUserId).HasColumnName("ToUserId");
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate");

            builder.OwnsOne(x => x.OrderMessage, x =>
            {
                x.ToTable("OrderMessages", "chat");
                x.WithOwner().HasForeignKey(y => y.MessageId);
                x.HasKey(y => new { y.ResponseId, y.MessageId });
            });
        }
    }
}