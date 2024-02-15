using Choice.Application.UseCases.Orders.GetOrder;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Orders.GetOrder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller, IOutputPort
    {
        private readonly IGetOrderUseCase _useCase;

        private IActionResult _viewModel;

        public OrderController(IGetOrderUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("{id}/Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
