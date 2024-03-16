using Choice.CompanyService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Choice.CompanyService.Api.Infrastructure.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.SocialMedias)
                   .HasConversion<string>(c => string.Join(":",c), s => s.Split(new[] { ':' }).AsReadOnly());

            builder.Property(c => c.PhotoUris)
                   .HasConversion<string>(c => string.Join(":", c), s => s.Split(new[] { ':' }).AsReadOnly());

            builder.Property(c => c.CategoriesId)
                   .HasConversion<string>(c => string.Join(":", c), s => 
                        s.Split(new[] { ':' }).Select(s => int.Parse(s)).ToList());
        }
    }
}
