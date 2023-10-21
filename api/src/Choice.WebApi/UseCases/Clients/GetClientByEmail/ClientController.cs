using Choice.Application.UseCases.Clients.GetClientByEmail;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Clients.GetClientByEmail
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller, IOutputPort
    {
        private readonly IGetClientByEmailUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientByEmailUseCase useCase)
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

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetClientByEmail(string email)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(email);

            return _viewModel;
        }
    }
}
