using Choice.Application.UseCases.Clients.GetClients;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Clients.GetClients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller, IOutputPort
    {
        private readonly IGetClientsUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientsUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<Client> clients)
        {
            _viewModel = Ok(clients);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
