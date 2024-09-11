using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Application.OrderRequests.Commands.CreateOrderRequest;

namespace WebApi.Modules.Users.OrderRequests
{
    [Controller]
    [Route("api/orderRequests")]
    public class OrderRequestController : Controller
    {
        private readonly IUsersModule _usersModule;

        public OrderRequestController(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }
        
        [HasPermission(Permissions.CreateOrderRequest)]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestRequest request)
        {
            await _usersModule.ExecuteCommand(new CreateOrderRequestCommand(
                request.ToKnowPrice,
                request.ToKnowDeadline,
                request.ToKnowEnrollmentDate,
                request.Distance,
                request.PhotoUris,
                request.Description,
                request.CategoryId));

            return Ok();
        }
    }
}