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

            builder.OwnsMany<Device>("_devices", y =>
            {
                y.ToTable("Devices", "chat");

                y.HasKey(z => new { z.UserId, z.Name });
                
                y.Property<string>("_token").HasColumnName("Token");
                y.Property<DateTime>("_expirationDate").HasColumnName("ExpirationDate");
                
                y.WithOwner().HasForeignKey("UserId");
            });
        }
    }
}