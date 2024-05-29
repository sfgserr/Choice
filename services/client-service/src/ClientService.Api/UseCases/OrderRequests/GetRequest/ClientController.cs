using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.GetRequest;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.OrderRequests.GetRequest
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IGetRequestUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetRequestUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(OrderRequest request, bool isUserCompany)
        {
            _viewModel = Ok(
                isUserCompany ? new OrderRequestDetailsViewModel(request) : new OrderRequestViewModel(request));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("GetOrderRequest")]
        public async Task<IActionResult> GetRequest(int id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
