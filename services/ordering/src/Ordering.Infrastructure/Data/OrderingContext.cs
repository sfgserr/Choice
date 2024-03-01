using Choice.Ordering.Domain.OrderEntity;
using Microsoft.EntityFrameworkCore;

namespace Choice.Ordering.Infrastructure.Data
{
    public sealed class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
    }
}
