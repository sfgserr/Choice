using Choice.Application.UseCases.Orders.UpdateOrder;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Orders.UpdateOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller, IOutputPort
    {
        private readonly IUpdateOrderUseCase _useCase;

        private IActionResult _viewModel;

        public OrderController(IUpdateOrderUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
        }

        [Route("Update")]
        public async Task<IActionResult> Update(Order order)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(order);

            return _viewModel;
        }
    }
}
