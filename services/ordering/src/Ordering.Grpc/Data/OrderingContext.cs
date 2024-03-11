using Microsoft.EntityFrameworkCore;

namespace Ordering.Grpc.Data
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<>
    }
}
