using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.CreateOrder
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void Invalid();
    }
}
