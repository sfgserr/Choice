using BuildingBlocks.Infrastructure.Authorization;
using Chat.Application.Contracts;
using Chat.Application.Messages.Commands.CreateMessage;
using Chat.Application.Messages.Queries.GetChat;
using Chat.Application.Messages.Queries.GetChats;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Chat.Messages
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : Controller
    {
        private readonly IChatModule _chatModule;

        public MessageController(IChatModule chatModule)
        {
            _chatModule = chatModule;
        }

        [HttpPost()]
        [HasPermission(Permissions.CreateMessage)]
        public async Task<IActionResult> CreateMessage(CreateMessageRequest request)
        {
            await _chatModule.ExecuteCommand(new CreateMessageCommand(
                request.Content,
                request.ToUserId,
                request.Type));

            return Ok();
        }
        
        
        [HttpGet("{toUserId:guid}")]
        [HasPermission(Permissions.GetChat)]
        public async Task<IActionResult> GetChat(Guid toUserId)
        {
            var chat = await _chatModule
                .Query<GetChatQuery, IEnumerable<MessageDto>>(new GetChatQuery(toUserId));

            return Ok(chat);
        }
        
        [HttpGet()]
        [HasPermission(Permissions.GetChats)]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatModule
                .Query<GetChatsQuery, IEnumerable<ChatDto>>(new GetChatsQuery());

            return Ok(chats);
        }
    }
}