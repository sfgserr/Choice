using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.GetClientRequests;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.OrderRequests.GetClientRequests
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IGetClientRequestsUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientRequestsUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.Ok(IList<OrderRequest> requests)
        {
            _viewModel = Ok(requests.Select(r => new OrderRequestViewModel(r)));
        }

        [HttpGet("GetClientRequests")]
        public async Task<IActionResult> GetClientRequests()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
