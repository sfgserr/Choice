using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Application.Users.Clients.Commands.CreateClient;

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
    }
}
