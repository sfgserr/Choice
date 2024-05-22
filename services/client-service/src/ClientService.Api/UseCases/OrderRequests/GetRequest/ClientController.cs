using Microsoft.AspNetCore.Mvc;
using Choice.ClientService.Application.UseCases.GetRequest;
using Choice.ClientService.Domain.OrderRequests;
using Choice.ClientService.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;

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

        void IOutputPort.Ok(OrderRequest request)
        {
            _viewModel = Ok(new OrderRequestDetailsViewModel(request));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("GetRequest")]
        public async Task<IActionResult> GetRequest(int id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
