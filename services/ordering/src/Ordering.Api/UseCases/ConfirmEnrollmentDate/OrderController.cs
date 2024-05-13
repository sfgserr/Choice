using Choice.Application.Services;
using Choice.Ordering.Application.UseCases.ConfirmEnrollmentDate;
using Choice.Ordering.Domain.OrderEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Ordering.Api.UseCases.ConfirmEnrollmentDate
{
    [ApiController]
    [Authorize("Company")]
    [Route("api/[controller]")]
    public class OrderController : Controller, IOutputPort
    {
        private readonly IConfirmEnrollmentDateUseCase _useCase;
        private readonly Notification _notification;

        private IActionResult _viewModel;

        public OrderController(IConfirmEnrollmentDateUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemDetails = new(_notification.ModelState);
            _viewModel = BadRequest(problemDetails);
        }

        [HttpPut("ConfirmEnrollmentDate")]
        public async Task<IActionResult> ConfirmEnrollmentDate(int orderId)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderId);

            return _viewModel;
        }
    }
}
