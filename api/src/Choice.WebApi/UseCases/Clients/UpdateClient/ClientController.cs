using Choice.Application.UseCases.Clients.UpdateClient;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Clients.UpdateClient
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : Controller, IOutputPort
    {
        private readonly IUpdateClientUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IUpdateClientUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(client);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Client client)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(client);

            return _viewModel;
        }
    }
}
