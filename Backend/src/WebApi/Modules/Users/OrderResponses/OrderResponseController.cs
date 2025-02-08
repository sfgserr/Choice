using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Application.OrderResponses.Commands.AddReview;
using Users.Application.OrderResponses.Commands.Cancel;
using Users.Application.OrderResponses.Commands.ChangeEnrollmentDate;
using Users.Application.OrderResponses.Commands.ConfirmDate;
using Users.Application.OrderResponses.Commands.Enroll;
using Users.Application.OrderResponses.Commands.Finish;
using Users.Application.OrderResponses.Commands.Response;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

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
        [HttpPost]
        public async Task<IActionResult> CreateOrderResponse(CreateOrderResponseRequest request)
        {
            await _usersModule.ExecuteCommand(new ResponseCommand(
                request.RequestId,
                request.Price,
                request.Deadline,
                request.EnrollmentDate,
                request.Prepayment));

            return Ok();
        }
        
        [HasPermission(Permissions.AddReview)]
        [HttpPost("review")]
        public async Task<IActionResult> AddReview(AddReviewRequest request)
        {
            await _usersModule.ExecuteCommand(new AddReviewCommand(
                request.ResponseId,
                request.ToUserId,
                request.Text,
                request.Grade));

            return Ok();
        }
        
        [HttpPut("cancel/{responseId:guid}")]
        [HasPermission(Permissions.Cancel)]
        public async Task<IActionResult> Cancel(Guid responseId)
        {
            await _usersModule.ExecuteCommand(new CancelCommand(
                responseId));

            return Ok();
        }
        
        [HttpPut("{responseId:guid}/{dateTime:datetime}")]
        [HasPermission(Permissions.ChangeEnrollmentDate)]
        public async Task<IActionResult> ChangeEnrollmentDate(Guid responseId, DateTime dateTime)
        {
            await _usersModule.ExecuteCommand(new ChangeEnrollmentDateCommand(
                responseId,
                dateTime));

            return Ok();
        }
        
        [HttpPut("confirm/{responseId:guid}")]
        [HasPermission(Permissions.ConfirmDate)]
        public async Task<IActionResult> Confirm(Guid responseId)
        {
            await _usersModule.ExecuteCommand(new ConfirmDateCommand(
                responseId));

            return Ok();
        }
        
        [HttpPut("enroll/{responseId:guid}")]
        [HasPermission(Permissions.Enroll)]
        public async Task<IActionResult> Enroll(Guid responseId)
        {
            await _usersModule.ExecuteCommand(new EnrollCommand(
                responseId));

            return Ok();
        }
        
        [HttpPut("finish/{responseId:guid}")]
        [HasPermission(Permissions.Finish)]
        public async Task<IActionResult> Finish(Guid responseId)
        {
            await _usersModule.ExecuteCommand(new FinishCommand(
                responseId));

            return Ok();
        }
        
        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.GetOrderResponse)]
        public async Task<IActionResult> GetOrderResponse(Guid id)
        {
            var orderResponse = await _usersModule.Query<GetOrderResponseQuery, OrderResponseDto>(
                new GetOrderResponseQuery(id));

            return Ok(orderResponse);
        }
    }
}