using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.SendOrderMessage
{
    public interface IOutputPort
    {
        void Ok(OrderMessage orderMessage);

        void Invalid();
    }
}
