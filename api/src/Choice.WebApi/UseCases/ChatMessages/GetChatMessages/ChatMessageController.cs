using Choice.Application.UseCases.ChatMessages.GetChatMessages;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.ChatMessages.GetChatMessages
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : Controller, IOutputPort
    {
        private readonly IGetChatMessagesUseCase _useCase;

        private IActionResult _viewModel;

        public ChatMessageController(IGetChatMessagesUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<ChatMessage> chat)
        {
            _viewModel = Ok(chat);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int user1Id, int user2Id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(user1Id, user2Id);

            return _viewModel;
        }
    }
}
