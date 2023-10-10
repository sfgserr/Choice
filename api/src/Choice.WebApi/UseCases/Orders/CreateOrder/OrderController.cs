using Choice.Application.UseCases.Orders.CreateOrder;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Orders.CreateOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller, IOutputPort
    {
        private readonly ICreateOrderUseCase _useCase;

        private IActionResult _viewModel;

        public OrderController(ICreateOrderUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Order order)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(order.Categories, order.Description, order.ToKnowPrice,
                                   order.ToKnowAppointmentTime, order.ToKnowDeadLine, order.PhotoUris,
                                   order.SearchingRadius);

            return _viewModel;
        }
    }
}
