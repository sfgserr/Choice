using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Clients.Commands.ChangeData;
using Users.Application.Clients.Commands.ChangeIconUri;
using Users.Application.Clients.Commands.CreateClient;
using Users.Application.Clients.Queries.GetClient;
using Users.Application.Contracts;

namespace WebApi.Modules.Users.Clients
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : Controller
    {
        private readonly IUsersModule _usersModule;

        public ClientController(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateClient(CreateClientRequest createClientRequest)
        {
            await _usersModule.ExecuteCommand(new CreateClientCommand(
                createClientRequest.Name,
                createClientRequest.Password,
                createClientRequest.Email,
                createClientRequest.PhoneNumber,
                createClientRequest.City,
                createClientRequest.Street));

            return Ok();
        }

        [HasPermission(Permissions.ChangeClientData)]
        [HttpPut()]
        public async Task<IActionResult> ChangeData(ChangeClientDataRequest request)
        {
            await _usersModule.ExecuteCommand(new ChangeDataCommand(
                request.Name,
                request.Email,
                request.PhoneNumber,
                request.City,
                request.Street));

            return Ok();
        }

        [HasPermission(Permissions.ChangeClientIconUri)]
        [HttpPut("{iconUri}")]
        public async Task<IActionResult> ChangeIconUri(string iconUri)
        {
            await _usersModule.ExecuteCommand(new ChangeIconUriCommand(iconUri));

            return Ok();
        }

        [HasPermission(Permissions.GetClient)]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var client = await _usersModule.Query<GetClientQuery, ClientDto>(new GetClientQuery());

            return Ok(client);
        }
    }
}
