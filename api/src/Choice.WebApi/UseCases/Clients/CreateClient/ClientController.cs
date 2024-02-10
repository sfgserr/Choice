using Choice.Application.UseCases.Clients.CreateClient;
using Choice.Domain.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Clients.CreateClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller, IOutputPort
    {
        private readonly ICreateClientUseCase _useCase;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public ClientController(ICreateClientUseCase useCase, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(client);
            _endPoint.Publish(new UserCreatedEvent(client.Id, client.Password, email: client.Email));
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
