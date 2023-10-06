using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.UpdateOrderMessage
{
    public class UpdateOrderMessagePresenter : IOutputPort
    {
        public OrderMessage? OrderMessage { get; set; }

        public void Ok(OrderMessage orderMessage)
        {
            OrderMessage = orderMessage;
        }
    }
}
