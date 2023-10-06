using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.GetOrder
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void NotFound();
    }
}
