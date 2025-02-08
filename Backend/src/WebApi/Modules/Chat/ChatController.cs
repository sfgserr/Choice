using Chat.Application.Chat.Queries.GetUserStatus;
using Chat.Application.Contracts;
using Chat.Application.Messages.Commands.Read;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Chat
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private readonly IChatModule _module;

        public ChatController(IChatModule module)
        {
            _module = module;
        }
        
        [HttpGet("status/{userId:guid}")]
        [HasPermission(Permissions.Chat)]
        public async Task<IActionResult> GetUserStatus(Guid userId)
        {
            var isOnline = await _module.Query<GetUserStatusQuery, bool>(new GetUserStatusQuery(userId));

            return Ok(isOnline);
        }
        
        [HttpPut("{userId:guid}/{messageId:guid}")]
        [HasPermission(Permissions.Read)]
        public async Task<IActionResult> Read(Guid userId, Guid messageId)
        {
            await _module.ExecuteCommand(new ReadCommand(userId, messageId));
            
            return Ok();
        }
    }
}