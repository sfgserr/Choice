using Chat.Application.Chat.Queries.GetUserStatus;
using Chat.Application.Contracts;
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
    }
}