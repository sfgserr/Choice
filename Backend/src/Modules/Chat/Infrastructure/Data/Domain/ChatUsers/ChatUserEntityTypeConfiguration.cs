using Chat.Domain.ChatUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Data.Domain.ChatUsers
{
    internal class ChatUserEntityTypeConfiguration : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.ToTable("ChatUsers", "chat");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_iconUri").HasColumnName("IconUri");
            builder.Property<bool>("_isDeleted").HasColumnName("IsDeleted");
        }
    }
}