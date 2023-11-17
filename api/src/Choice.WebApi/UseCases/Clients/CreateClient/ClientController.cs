using Choice.Application.UseCases.Clients.CreateClient;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Clients.CreateClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller, IOutputPort
    {
        private readonly ICreateClientUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(ICreateClientUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(client);
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Client client)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(client.Name, client.Surname, client.Password, client.Email, client.IconUri);

            return _viewModel;
        }
    }
}
