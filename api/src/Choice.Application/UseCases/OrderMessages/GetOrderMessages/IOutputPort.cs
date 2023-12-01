using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.GetOrderMessages
{
    public interface IOutputPort
    {
        void Ok(IList<OrderMessage> orderMessages);
    }
}
