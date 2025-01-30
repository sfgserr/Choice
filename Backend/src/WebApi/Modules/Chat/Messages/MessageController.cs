using Chat.Application.Contracts;
using Chat.Application.Messages.Commands.CreateMessage;
using Chat.Application.Messages.Commands.Read;
using Chat.Application.Messages.Queries.GetChat;
using Chat.Application.Messages.Queries.GetChats;
using GetChatDto = Chat.Application.Messages.Queries.GetChat.ChatDto;
using GetChatsDto = Chat.Application.Messages.Queries.GetChats.ChatDto;
using Identity.Infrastructure.Authorization;
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

        [HttpPost]
        [HasPermission(Permissions.CreateMessage)]
        public async Task<IActionResult> CreateMessage(CreateMessageRequest request)
        {
            await _chatModule.ExecuteCommand(new CreateMessageCommand(
                request.Content,
                request.ToUserId,
                request.Type));

            return Ok();
        }
        
        [HttpPut("{messageId:guid}")]
        [HasPermission(Permissions.Read)]
        public async Task<IActionResult> Read(Guid messageId)
        {
            await _chatModule.ExecuteCommand(new ReadCommand(messageId));
            
            return Ok();
        }
        
        [HttpGet("{toUserId:guid}")]
        [HasPermission(Permissions.GetChat)]
        public async Task<IActionResult> GetChat(Guid toUserId)
        {
            var chat = await _chatModule
                .Query<GetChatQuery, GetChatDto>(new GetChatQuery(toUserId));

            return Ok(chat);
        }
        
        [HttpGet]
        [HasPermission(Permissions.GetChats)]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatModule
                .Query<GetChatsQuery, IEnumerable<GetChatsDto>>(new GetChatsQuery());

            return Ok(chats);
        }
    }
}