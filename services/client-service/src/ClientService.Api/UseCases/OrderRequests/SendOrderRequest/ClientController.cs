﻿using Choice.Application.Services;
using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.SendOrderRequest;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.OrderRequests.SendOrderRequest
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly ISendOrderRequestUseCase _useCase;
        private readonly Notification _notification;

        private IActionResult _viewModel;

        public ClientController(ISendOrderRequestUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;
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

        void IOutputPort.Ok(OrderRequest request)
        {
            _viewModel = Ok(new OrderRequestViewModel(request));
        }

        [HttpPost("SendOrderRequest")]
        public async Task<IActionResult> SendOrderRequest([FromBody] SendOrderRequest request)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute
                (request.Description,
                 request.PhotoUris,
                 request.CategoryId,
                 request.SearchRadius,
                 request.ToKnowPrice,
                 request.ToKnowDeadline,
                 request.ToKnowEnrollmentDate);

            return _viewModel;
        }
    }
}
