using Choice.Chat.Entities;
using Choice.Chat.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Choice.Chat.Repositories;

namespace Choice.Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessageRepository _repository;

        public MessageController(IHubContext<ChatHub> hubContext, IMessageRepository repository)
        {
            _hubContext = hubContext;
            _repository = repository;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(string text, string receiverId)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            Message message = new(text, id, receiverId);

            await _repository.Add(message);

            await _hubContext.Clients.User(receiverId).SendAsync("ReceiveTextMessage", message);

            return Ok();
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string receiverId)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            IList<Message> messages = await _repository.GetAll(id, receiverId);

            return Ok(messages);
        }

        [HttpPut("EditMessage")]
        public async Task<IActionResult> EditMessage(int messageId, string text)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            Message message = await _repository.Get(messageId, id);

            if (message is null)
                return NotFound();

            message.EditText(text);

            await _repository.Update(message);

            return Ok();
        }
    }
}
