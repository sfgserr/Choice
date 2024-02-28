using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Infrastructure.Data
{
    public sealed class OrderingContextFake
    {
        public OrderingContextFake()
        {
            
        }

        public List<Order> Orders { get; set; } = new List<Order>();    
    }
}
