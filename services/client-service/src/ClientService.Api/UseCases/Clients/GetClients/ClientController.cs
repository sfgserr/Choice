using Choice.ClientService.Application.UseCases.GetClients;
using Choice.ClientService.Domain.ClientAggregate;
using ClientService.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.Clients.GetClients
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class ClientController : Controller, IOutputPort 
    {
        private readonly IGetClientsUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientsUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<Client> clients)
        {
            _viewModel = Ok(clients.Select(c => new ClientAdminViewModel(c)));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetClients()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
