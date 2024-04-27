using Azure.Core;
using Choice.Application.Services;
using Choice.ClientService.Api.ViewModels;
using Choice.ClientService.Application.UseCases.ChangeIconUri;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.ClientService.Api.UseCases.Clients.ChangeIconUri
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IChangeIconUriUseCase _useCase;
        private readonly Notification _notification;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public ClientController(IChangeIconUriUseCase useCase, Notification notification, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _notification = notification;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Client client)
        {
            _viewModel = Ok(new ClientAdminViewModel(client));
            _endPoint.Publish<UserIconUriChangenEvent>(new(client.Guid, client.IconUri));
        }

        void IOutputPort.Invalid()
        {
            ValidationProblemDetails problemDetails = new(_notification.ModelState);
            _viewModel = BadRequest(problemDetails);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        public async Task<IActionResult> ChangeIconUri(string iconUri)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(iconUri);

            return _viewModel;
        }
    }
}
