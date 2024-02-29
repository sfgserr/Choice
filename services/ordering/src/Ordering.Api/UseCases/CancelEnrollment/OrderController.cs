﻿using Choice.EventBus.Messages.Events;
using Choice.Ordering.Application.Services;
using Choice.Ordering.Application.UseCases.CancelEnrollment;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.UseCases.CancelEnrollment;

namespace Choice.Ordering.Api.UseCases.CancelEnrollment
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller, IOutputPort
    {
        private readonly ICancelEnrollmentUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public OrderController(ICancelEnrollmentUseCase useCase, Notification notification, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
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

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
            _endPoint.Publish(new OrderChangedEvent(order.Id, order.ReceiverId, "Cancel", order.SenderId));
        }

        [HttpPut("CancelEnrollment")]
        public async Task<IActionResult> CancelEnrollment(int orderId)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(orderId);

            return _viewModel;
        }
    }
}
