using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Choice.Infrastructure
{
    public class ContextFactory : IDesignTimeDbContextFactory<ChoiceContext>
    {
        private readonly IConfiguration _configuration;

        public ContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ChoiceContext CreateDbContext(string[] args)
        {
            string connectionString = _configuration["Database:ConnectionString"];

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            builder.UseSqlServer(connectionString);

            return new ChoiceContext(builder.Options);
        }
    }
}
