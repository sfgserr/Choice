using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.ViewModels;

namespace Choice.Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class MessageController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessageRepository _messageRepository;

        public MessageController(IHubContext<ChatHub> hubContext, IMessageRepository messageRepository)
        {
            _hubContext = hubContext;
            _messageRepository = messageRepository;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMessage(string text, string receiverId)
        {
            string id = User.FindFirstValue("id")!;

            Message message = new(id, receiverId, text, MessageType.Text);

            await _messageRepository.Add(message);

            await _hubContext.Clients.Client(message.ReceiverId).SendAsync("onSend", new MessageViewModel(message));

            return Ok();
        }

        [HttpPut("Read")]
        public async Task<IActionResult> Read(int id)
        {
            Message? message = await _messageRepository.Get(id);

            if (message is not null)
            {
                message.Read();

                _messageRepository.Update(message);

                await _hubContext.Clients.Client(message.SenderId).SendAsync("read", new { message.Id });

                return Ok();
            }

            return NotFound();
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string receiverId)
        {
            string id = User.FindFirstValue("id")!;

            IList<Message> messages = await _messageRepository.GetAll(id, receiverId);

            return Ok(messages);
        }

        [HttpGet("GetChats")]
        public async Task<IActionResult> GetChats()
        {
            string id = User.FindFirstValue("id")!;

            IList<Message> messages = await _messageRepository.GetAll();

            return Ok(messages.Where(m => m.SenderId == id || m.ReceiverId == id).Select(m => m.Receiver));
        }
    }
}
