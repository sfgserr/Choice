using Choice.Application.UseCases.OrderMessages.UpdateOrderMessage;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.OrderMessages.UpdateOrderMessage
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMessageController : Controller, IOutputPort
    {
        private readonly IUpdateOrderMessageUseCase _useCase;

        private IActionResult _viewModel;

        public OrderMessageController(IUpdateOrderMessageUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(OrderMessage orderMessage)
        {
            _viewModel = Ok(orderMessage);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(OrderMessage orderMessage)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderMessage);

            return _viewModel;
        }
    }
}
