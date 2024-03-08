using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.GetOrderRequests;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.OrderRequests.GetOrderRequests
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ClientController : Controller, IOutputPort
    {
        private readonly IGetOrderRequestsUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetOrderRequestsUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<OrderRequest> requests) 
        {
            _viewModel = Ok(requests.Select(r => new OrderRequestDetailsViewModel(r)));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("GetOrderRequests")]
        public async Task<IActionResult> GetOrderRequests()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
