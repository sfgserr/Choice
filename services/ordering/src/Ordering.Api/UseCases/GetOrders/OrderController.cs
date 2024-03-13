using Choice.Ordering.Application.UseCases.GetOrders;
using Choice.Ordering.Domain.OrderEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Ordering.Api.UseCases.GetOrders
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class OrderController : Controller, IOutputPort
    {
        private readonly IGetOrdersUseCase _useCase;

        private IActionResult _viewModel;

        public OrderController(IGetOrdersUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<Order> orders)
        {
            _viewModel = Ok(orders);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetOrders()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
