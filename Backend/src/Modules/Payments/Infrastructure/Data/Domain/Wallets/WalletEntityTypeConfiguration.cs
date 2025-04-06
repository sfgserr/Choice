using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Wallets;

namespace Payments.Infrastructure.Data.Domain.Wallets
{
    internal class WalletEntityTypeConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets", "payments");
            
            builder.HasKey(x => new { x.Id, x.PayerId });

            builder.Property<int>("_copecks").HasColumnName("Copecks");
        }
    }
}