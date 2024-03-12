using Choice.Application.Services;
using Choice.EventBus.Messages.Events;
using Choice.Ordering.Application.UseCases.Enroll;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Ordering.Api.UseCases.Enroll
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Client")]
    public sealed class OrderController : Controller, IOutputPort
    {
        private readonly IEnrollUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public OrderController(IEnrollUseCase useCase, Notification notification, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
            _endPoint.Publish(new OrderChangedEvent(order.Id, order.ReceiverId, "Enroll", order.SenderId));
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

        [HttpPut("Enroll")]
        public async Task<IActionResult> Enroll(int orderId)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderId);

            return _viewModel;
        }
    }
}
