﻿using Choice.Application.Services;
using Choice.EventBus.Messages.Events;
using Choice.Ordering.Application.UseCases.ConfirmEnrollmentDate;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;
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
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public OrderController(IConfirmEnrollmentDateUseCase useCase, Notification notification, 
            IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Order order)
        {
            _viewModel = Ok(order);
            _endPoint.Publish(new OrderEnrollmentDateConfirmedEvent(order.Id, order.ClientId));
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
