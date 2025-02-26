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
            
            builder.Property(x => x.Type)
                .HasColumnName("Type")
                .HasConversion(x => x.Value, x => MessageType.Parse(x));
            builder.Property(x => x.Body).HasColumnName("Body");
            builder.Property(x => x.FromUserId).HasColumnName("FromUserId");
            builder.Property(x => x.ToUserId).HasColumnName("ToUserId");
            builder.Property(x => x.IsRead).HasColumnName("IsRead");
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate");
            
            builder.OwnsOne(x => x.OrderMessage, x =>
            {
                x.ToTable("OrderMessages", "chat");
                
                x.HasKey(y => y.MessageId);
                x.WithOwner().HasForeignKey(y => y.MessageId);
                x.Property(y => y.ResponseId).HasColumnName("ResponseId");
                x.Property<DateTime?>("_enrollmentDate").HasColumnName("EnrollmentDate");
                x.Property<bool>("_isActive").HasColumnName("IsActive");
            });
            
            builder.Navigation(x => x.OrderMessage).AutoInclude();
        }
    }
}