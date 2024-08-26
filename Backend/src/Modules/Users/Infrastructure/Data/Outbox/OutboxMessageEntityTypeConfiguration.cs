using BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users.Infrastructure.Data.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", "users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).HasColumnName("Type");
            builder.Property(x => x.Message).HasColumnName("Message");
            builder.Property(x => x.OccuredOn).HasColumnName("OccuredOn");
        }
    }
}
