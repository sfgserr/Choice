using ClientService.Application.UseCases.DeleteClientAdmin;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Api.UseCases.Clients.DeleteClientAdmin
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Admin")]
    public sealed class ClientController : Controller, IOutputPort
    {
        private readonly IDeleteClientAdminUseCase _useCase;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _result;

        public ClientController(IDeleteClientAdminUseCase useCase, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(string id)
        {
            _result = Ok();
            _endPoint.Publish(new UserDeletedEvent(id));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _result;
        }
    }
}
