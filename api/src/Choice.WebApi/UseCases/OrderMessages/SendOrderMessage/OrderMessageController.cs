using Choice.Application.UseCases.OrderMessages.SendOrderMessage;
using Choice.Domain.Models;
using Choice.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Choice.WebApi.UseCases.OrderMessages.SendOrderMessage
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMessageController : Controller, IOutputPort
    {
        private readonly ISendOrderMessageUseCase _useCase;
        private readonly IHubContext<ChatHub> _hubContext; 

        private IActionResult _viewModel;

        public OrderMessageController(ISendOrderMessageUseCase useCase, IHubContext<ChatHub> hubContext)
        {
            _useCase = useCase;
            _hubContext = hubContext;
        }

        async void IOutputPort.Ok(OrderMessage orderMessage)
        {
            _viewModel = Ok(orderMessage);
            await _hubContext.Clients.All.SendAsync("Receive", orderMessage.Room.Id, orderMessage);
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
