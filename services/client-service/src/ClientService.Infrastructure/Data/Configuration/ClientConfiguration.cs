using Choice.ClientService.Domain.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientService.Infrastructure.Data.Configuration
{
    public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.HasMany(c => c.Requests)
                   .WithOne(r => r.Client!)
                   .HasForeignKey(r => r.ClientId);

            builder.OwnsOne(c => c.Address);
        }
    }
}
