using Choice.Chat.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.ViewModels;
using Choice.Chat.Api.Services;

namespace Choice.Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class MessageController : Controller
    {
        private readonly ChatService _chatService;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageController(IMessageRepository messageRepository, ChatService chatService, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _chatService = chatService;
            _userRepository = userRepository;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMessage(string text, string receiverId)
        {
            string id = User.FindFirstValue("id")!;

            Message message = new(id, receiverId, text, MessageType.Text);

            await _messageRepository.Add(message);

            await _chatService.SendMessage(message.ReceiverId, "onSend", message);

            return Ok();
        }

        [HttpPut("Read")]
        public async Task<IActionResult> Read(int id)
        {
            Message? message = await _messageRepository.Get(id);

            if (message is not null)
            {
                message.Read();

                await _messageRepository.Update(message);

                await _chatService.SendMessage(message.SenderId, "read", message);

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
            string userId = User.FindFirstValue("id")!;

            IList<Message> messages = await _messageRepository.GetAll();

            IEnumerable<string> ids = messages.Where(m => m.SenderId == userId || m.ReceiverId == userId)
                                .SelectMany(m => new[] { m.SenderId, m.ReceiverId })
                                .Distinct()
                                .Where(i => i != userId);

            List<ChatViewModel> chats = [];

            foreach (string id in ids) 
            {
                User user = (await _userRepository.Get(id))!;

                Message lastMessage = messages.Where(m => (m.SenderId == id || m.ReceiverId == id) && 
                                      (m.SenderId == userId || m.ReceiverId == userId)).Last();

                chats.Add(new ChatViewModel(user.Name, 
                                            user.IconUri, 
                                            lastMessage.Type == MessageType.Text ? lastMessage.Body : "Заказ", 
                                            id, 
                                            lastMessage.IsRead, 
                                            lastMessage.CreationTime, 
                                            lastMessage.ReceiverId == userId));
            }

            return Ok(chats);
        }
    }
}
