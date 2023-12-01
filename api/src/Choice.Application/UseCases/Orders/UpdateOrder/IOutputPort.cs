using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.UpdateOrder
{
    public interface IOutputPort
    {
        void Ok(Order order);
    }
}
