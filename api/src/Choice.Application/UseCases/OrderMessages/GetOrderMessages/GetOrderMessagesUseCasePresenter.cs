using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.GetOrderMessages
{
    public class GetOrderMessagesUseCasePresenter : IOutputPort
    {
        public IList<OrderMessage> OrderMessages { get; set; }

        public void Ok(IList<OrderMessage> orderMessages)
        {
            OrderMessages = orderMessages;
        }
    }
}
