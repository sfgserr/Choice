using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Application.OrderRequests.Commands.ChangeOrderRequest;
using Users.Application.OrderRequests.Commands.CreateOrderRequest;
using Users.Application.OrderRequests.Queries.GetOrderRequest;
using Users.Application.OrderRequests.Queries.GetOrderRequests;
using Users.Application.OrderRequests.Queries.GetOrderRequestsInRadius;
using GetOrderRequestDto = Users.Application.OrderRequests.Queries.GetOrderRequest.OrderRequestDto;
using GetOrderRequestsDto = Users.Application.OrderRequests.Queries.GetOrderRequests.OrderRequestDto;
using GetOrderRequestsInRadiusDto = Users.Application.OrderRequests.Queries.GetOrderRequestsInRadius.OrderRequestDto;
using CreateOrderRequestDto = Users.Application.OrderRequests.Commands.CreateOrderRequest.OrderRequestDto;

namespace WebApi.Modules.Users.OrderRequests
{
    [ApiController]
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
            var orderRequest = await _usersModule.ExecuteCommand<CreateOrderRequestCommand, CreateOrderRequestDto>(
                new CreateOrderRequestCommand(
                    request.ToKnowPrice,
                    request.ToKnowDeadline,
                    request.ToKnowEnrollmentDate, 
                    request.Distance,
                    request.PhotoUris,
                    request.Description,
                    request.CategoryId));

            return Ok(orderRequest);
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
        
        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.GetOrderRequest)]
        public async Task<IActionResult> GetOrderRequest(Guid id)
        {
            var request = await _usersModule.Query<GetOrderRequestQuery, GetOrderRequestDto>(new GetOrderRequestQuery(id));

            return Ok(request);
        }
        
        [HttpGet()]
        [HasPermission(Permissions.GetOrderRequests)]
        public async Task<IActionResult> GetOrderRequests()
        {
            var requests = await 
                _usersModule.Query<GetOrderRequestsQuery, IEnumerable<GetOrderRequestsDto>>(new GetOrderRequestsQuery());

            return Ok(requests);
        }
        
        [HttpGet("radius")]
        [HasPermission(Permissions.GetOrderRequestsInRadius)]
        public async Task<IActionResult> GetOrderRequestsInRadius()
        {
            var requests = await _usersModule
                .Query<GetOrderRequestsInRadiusQuery, IEnumerable<GetOrderRequestsInRadiusDto>>(
                    new GetOrderRequestsInRadiusQuery());

            return Ok(requests);
        }
    }
}