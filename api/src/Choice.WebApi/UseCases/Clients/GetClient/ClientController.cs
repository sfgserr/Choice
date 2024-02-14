using Choice.Application.UseCases.Clients.GetClient;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Clients.GetClient
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : Controller, IOutputPort
    {
        private readonly IGetClientUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(client);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("{id}/Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
