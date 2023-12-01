using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.GetOrderMessages
{
    public class GetOrderMessagesPresenter : IOutputPort
    {
        public IList<OrderMessage> OrderMessages { get; set; } = new List<OrderMessage>();

        public void Ok(IList<OrderMessage> orderMessages)
        {
            OrderMessages = orderMessages;
        }
    }
}
