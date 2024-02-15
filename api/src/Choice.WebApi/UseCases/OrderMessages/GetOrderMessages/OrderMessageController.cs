using Choice.Application.UseCases.OrderMessages.GetOrderMessages;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.OrderMessages.GetOrderMessages
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderMessageController : Controller, IOutputPort
    {
        private readonly IGetOrderMessagesUseCase _useCase;

        private IActionResult _viewModel;

        public OrderMessageController(IGetOrderMessagesUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<OrderMessage> orderMessages)
        {
            _viewModel = Ok(orderMessages);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int user1Id, int user2Id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(user1Id, user2Id);

            return _viewModel;
        }
    }
}
