using Choice.Application.Services;
using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.ChangeOrderRequest;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.OrderRequests.ChangeOrderRequest
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IChangeOrderRequestUseCase _useCase;
        private readonly Notification _notification;

        private IActionResult _viewModel;

        public ClientController(IChangeOrderRequestUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;
        }

        void IOutputPort.Ok(OrderRequest request)
        {
            _viewModel = Ok(new OrderRequestViewModel(request));
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

        [HttpPut("ChangeOrderRequest")]
        public async Task<IActionResult> ChangeOrderRequest([FromBody] ChangeOrderRequest request)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute
                (request.Id, 
                 request.Description, 
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
