using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Application.OrderResponses.Commands.Response;

namespace WebApi.Modules.Users.OrderResponses
{
    [ApiController]
    [Route("api/orderResponses")]
    public class OrderResponseController : Controller
    {
        private readonly IUsersModule _usersModule;

        public OrderResponseController(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }
        
        [HasPermission(Permissions.Response)]
        [HttpPost()]
        public async Task<IActionResult> Response(ResponseRequest request)
        {
            await _usersModule.ExecuteCommand(new ResponseCommand(
                request.RequestId,
                request.Price,
                request.Deadline,
                request.EnrollmentDate,
                request.Prepayment));

            return Ok();
        }
    }
}