using BuildingBlocks.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users.Infrastructure.Data.InternalCommands
{
    internal class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
    {
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("InternalCommands", "users");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).HasColumnName("Type");
            builder.Property(x => x.Data).HasColumnName("Data");
        }
    }
}
