using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Infrastructure.Data
{
    public sealed class OrderingContextFake
    {
        public OrderingContextFake()
        {
            Orders.Add(new Order(1, "1", "2", 100, 0, 3600, new DateTime(2024, 2, 29)));
        }

        public List<Order> Orders { get; set; } = new List<Order>();    
    }
}
