using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Application.OrderRequests.Commands.ChangeOrderRequest;
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
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
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

        [HttpPut()]
        [HasPermission(Permissions.ChangeOrderRequest)]
        public async Task<IActionResult> ChangeOrderRequest(ChangeOrderRequest request)
        {
            await _usersModule.ExecuteCommand(new ChangeOrderRequestCommand(
                request.RequestId,
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