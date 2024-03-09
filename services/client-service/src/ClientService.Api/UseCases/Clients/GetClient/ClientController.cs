using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.GetClient;
using Choice.ClientService.Domain.ClientAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.Clients.GetClient
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IGetClientUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(new ClientAdminViewModel(client));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("GetClient")]
        public async Task<IActionResult> GetClient()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
