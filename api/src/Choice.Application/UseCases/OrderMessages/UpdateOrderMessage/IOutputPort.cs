using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.UpdateOrderMessage
{
    public interface IOutputPort
    {
        void Ok(OrderMessage orderMessage);
    }
}
