using BuildingBlocks.Infrastructure.Data.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identity.Infrastructure.Data.DesignTime
{
    internal class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            
            if (args.Length == 0) throw new ArgumentException("Please provide database connection string");
            
            optionsBuilder.UseNpgsql(args[0]);
            optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
            optionsBuilder.UseOpenIddict();
                
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}