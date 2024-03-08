using Choice.ClientService.Application.Services;
using Choice.ClientService.Application.UseCases.ChangeUserData;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.EventBus.Messages.Events;
using Choice.ClientService.Api.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.Clients.ChangeUserData
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IChangeUserDataUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public ClientController(IChangeUserDataUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(new ClientAdminViewModel(client));
            _endPoint.Publish<UserDataChangedEvent>(new(client.Name, client.Surname));
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemDetails = new(_notification.ModelState);
            _viewModel = BadRequest(problemDetails);
        }

        [HttpPut("ChangeUserData")]
        public async Task<IActionResult> ChangeUserData(string name, string surname)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(name, surname);

            return _viewModel;
        }
    }
}
