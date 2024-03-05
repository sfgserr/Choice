using Choice.ClientService.Application.UseCases.GetClients;
using Choice.ClientService.Domain.ClientAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.Clients.GetClients
{
    [ApiController]
    [Route("api/[controller]")]
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

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetClients()
        {

        }
    }
}
