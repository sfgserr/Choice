using Choice.Application.Services;
using Choice.EventBus.Messages.Events;
using Choice.Ordering.Application.UseCases.FinishOrder;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Ordering.Api.UseCases.FinishOrder
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class OrderController : Controller, IOutputPort
    {
        private readonly IFinishOrderUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public OrderController(IFinishOrderUseCase useCase, Notification notification, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
            _endPoint.Publish(new OrderStatusChangedEvent
                (order.OrderRequestId,
                 (int)order.Status));
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

        [HttpPut("Finish")]
        public async Task<IActionResult> FinishOrder(int orderId)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderId);

            return _viewModel;
        }
    }
}
