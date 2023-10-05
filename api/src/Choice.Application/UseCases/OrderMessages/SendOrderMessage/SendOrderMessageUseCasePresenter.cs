using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.SendOrderMessage
{
    public class SendOrderMessageUseCasePresenter : IOutputPort
    {
        public bool IsInvalid { get; set; } 
        public OrderMessage OrderMessage { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(OrderMessage orderMessage)
        {
            OrderMessage = orderMessage;
        }
    }
}
