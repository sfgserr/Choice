using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;
using Choice.Infrastructure.Data;
using ClientService.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Choice.ClientService.Infrastructure.Data
{
    public class ClientContext : DbContext, IContext
    {
        public ClientContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public async Task SaveEntities()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderRequest> Requests { get; set; }
    }
}
