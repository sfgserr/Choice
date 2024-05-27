using Choice.Infrastructure.Data;
using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Infrastructure.Data
{
    public sealed class OrderingContextFake
    {
        public OrderingContextFake()
        {
            Order order = new
                (1, SeedData.DefaultCompanyGuid, SeedData.DefaultClientGuid, 100, 0, 3600, DateTime.UtcNow);

            Orders.Add(order);
        }

        public List<Order> Orders { get; set; } = new List<Order>();    
    }
}
