using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.EntityFrameworkCore;

namespace Choice.ClientService.Infrastructure.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderRequest> Requests { get; set; }
    }
}
