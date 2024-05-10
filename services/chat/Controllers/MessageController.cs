using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Choice.Chat.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Choice.Chat.Api.Repositories.Interfaces;

namespace Choice.Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class MessageController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IRepository<Message> _messageRepository;

        public MessageController(IHubContext<ChatHub> hubContext, IRepository<Message> messageRepository)
        {
            _hubContext = hubContext;
            _messageRepository = messageRepository;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(string text, int groupId)
        {
            string id = User.FindFirstValue("id")!;

            Message message = new(text, id, groupId);

            await _messageRepository.Add(message);

            await _hubContext.Clients.Group();

            return Ok();
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string receiverId)
        {
            string id = User.FindFirstValue("id")!;

            IList<Message> messages = await _messageRepository.GetAll(id, receiverId);

            return Ok(messages);
        }
    }
}
