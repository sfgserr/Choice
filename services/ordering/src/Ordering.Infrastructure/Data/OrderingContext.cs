using Choice.Infrastructure.Data;
using Choice.Ordering.Domain.OrderEntity;
using Microsoft.EntityFrameworkCore;

namespace Choice.Ordering.Infrastructure.Data
{
    public sealed class OrderingContext : DbContext, IContext
    {
        public OrderingContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public async Task SaveEntities() =>
            await SaveChangesAsync();

        public DbSet<Order> Orders { get; set; }
    }
}
