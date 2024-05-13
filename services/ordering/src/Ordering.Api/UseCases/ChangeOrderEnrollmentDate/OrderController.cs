using Choice.Application.Services;
using Choice.EventBus.Messages.Events;
using Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Ordering.Api.UseCases.ChangeOrderEnrollmentDate
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class OrderController : Controller, IOutputPort
    {
        private readonly IChangeOrderEnrollmentDateUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public OrderController(IChangeOrderEnrollmentDateUseCase useCase, Notification notification, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Order order, string receiverId)
        {
            _viewModel = Ok(order);
            _endPoint.Publish(new OrderEnrollmentDateChangedEvent(order.Id, order.EnrollmentDate, receiverId));
        }

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemDetails = new(_notification.ModelState);
            _viewModel = BadRequest(problemDetails);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpPut("ChangeOrderEnrollmentDate")]
        public async Task<IActionResult> ChangeOrderEnrollmentDate(int orderId, DateTime newDate)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderId, newDate);

            return _viewModel;
        }
    }
}
