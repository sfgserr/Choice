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

            await _chatService.SendMessage(message.ReceiverId, "send", new(message));

            return Ok(new MessageViewModel(message));
        }

        [HttpPut("Read")]
        public async Task<IActionResult> Read(int id)
        {
            Message? message = await _messageRepository.Get(id);

            if (message is not null)
            {
                message.Read();

                await _messageRepository.Update(message);

                await _chatService.SendMessage(message.SenderId, "read", new(message));

                return Ok();
            }

            return NotFound();
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string receiverId)
        {
            string id = User.FindFirstValue("id")!;

            IList<Message> messages = await _messageRepository.GetAll(id, receiverId);

            return Ok(messages.Select(m => new MessageViewModel(m)));
        }

        [HttpGet("GetChat")]
        public async Task<IActionResult> GetChat(string userId)
        {
            string id = User.FindFirstValue("id")!;

            User? user = await _userRepository.Get(userId);

            if (user is not null)
            {
                IList<Message> messages = await _messageRepository.GetAll(id, userId);
                messages = messages.OrderBy(m => m.CreationTime).ToList();

                ChatViewModel chat = new(user.Name,
                                         user.IconUri,
                                         user.Guid,
                                         messages.Select(m => new MessageViewModel(m)).ToList());

                return Ok(chat);
            }

            return NotFound();
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

                IList<Message> chatMessages = await _messageRepository.GetAll(userId, id);

                chats.Add(new(user.Name, 
                              user.IconUri, 
                              id,
                              chatMessages.Select(m => new MessageViewModel(m)).ToList()));
            }

            return Ok(chats);
        }
    }
}
