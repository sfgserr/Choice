using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.GetClientAdmin;
using Choice.ClientService.Domain.ClientAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.Clients.GetClientAdmin
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Admin")]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IGetClientAdminUseCase _useCase;

        private IActionResult _viewModel;

        public ClientController(IGetClientAdminUseCase useCase)
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

        [HttpGet("GetClientAdmin")]
        public async Task<IActionResult> GetClient(string id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
