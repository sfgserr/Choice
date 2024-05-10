using Choice.Chat.Api.Entities;

namespace Choice.Chat.Api.Factories
{
    public sealed class OrderFactory
    {
        public Order Copy(Order order) =>
            new(order.OrderId, order.Price, order.Deadline, order.EnrollmentDate, order.Prepayment, order.SenderId, 
                order.ReceiverId);
    }
}
