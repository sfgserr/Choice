using Choice.EventBus.Messages.Events;
using Choice.Ordering.Application.Services;
using Choice.Ordering.Application.UseCases.CreateOrder;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.Ordering.Api.UseCases.CreateOrder
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : Controller, IOutputPort 
    {
        private readonly ICreateOrderUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public OrderController(ICreateOrderUseCase useCase, Notification notification,
            IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);

            _endPoint.Publish(new OrderCreatedEvent(
                order.Id,
                order.OrderRequestId,
                order.SenderId, 
                order.ReceiverId, 
                order.Price, 
                order.Prepayment, 
                order.Deadline, 
                order.IsEnrolled, 
                order.EnrollmentDate, 
                order.Status.ToString()));
        }

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemtDetails = new(_notification.ModelState);
            _viewModel = BadRequest(problemtDetails);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(
                request.ReceiverId,
                request.OrderRequestId,
                request.Price,
                request.Prepayment,
                request.Deadline,
                request.EnrollmentTime);

            return _viewModel;
        }
    }
}
