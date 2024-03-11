using Choice.Ordering.Domain.OrderEntity;

namespace Ordering.Api.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Order order)
        {
            Id = order.Id;
            Reviews = order.Reviews;
        }

        public int Id { get; }
        public string[] Reviews { get; }
    }
}
