using Choice.Application.UseCases.OrderMessages.SendOrderMessage;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.OrderMessages.SendOrderMessage
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMessageController : Controller, IOutputPort
    {
        private readonly ISendOrderMessageUseCase _useCase;

        private IActionResult _viewModel;

        public OrderMessageController(ISendOrderMessageUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(OrderMessage orderMessage)
        {
            _viewModel = Ok(orderMessage);
        }

        void IOutputPort.Invalid() 
        {
            _viewModel = BadRequest();
        }  

        [Route("Send")]
        public async Task<IActionResult> Send(OrderMessage orderMessage)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderMessage.Sender, orderMessage.Room, orderMessage.Price,
                                   orderMessage.AppointmentTime, orderMessage.Duration, orderMessage.Order);

            return _viewModel;
        }
    }
}
