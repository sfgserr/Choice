using Chat.Domain.ChatUsers;
using Chat.Domain.Messages;
using Chat.Domain.Messages.OrderMessages;
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
            
            builder.Property<MessageType>("_type")
                .HasColumnName("Type")
                .HasConversion(x => x.Value, x => MessageType.Parse(x));
            builder.Property<string>("_body").HasColumnName("Body");
            builder.Property<ChatUserId>("_fromUserId").HasColumnName("FromUserId");
            builder.Property<ChatUserId>("_toUserId").HasColumnName("ToUserId");
            builder.Property<bool>("_isRead").HasColumnName("IsRead");
            builder.Property<DateTime>("_creationDate").HasColumnName("CreationDate");
            builder.OwnsOne<OrderMessage>("_orderMessage", x =>
            {
                x.ToTable("OrderMessages", "chat");
                
                x.HasKey(y => y.MessageId);
                x.WithOwner().HasForeignKey(y => y.MessageId);
                x.Property(y => y.OrderResponseId).HasColumnName("ResponseId");
                x.Property<DateTime?>("_enrollmentDate").HasColumnName("EnrollmentDate");
                x.Property<bool>("_isActive").HasColumnName("IsActive");
            });
            
            builder.Navigation("_orderMessage").AutoInclude();
        }
    }
}