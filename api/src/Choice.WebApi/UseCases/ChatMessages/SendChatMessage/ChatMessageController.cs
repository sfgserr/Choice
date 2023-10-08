using Choice.Application.UseCases.Messages.SendChatMessage;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.ChatMessages.SendChatMessage
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : Controller, IOutputPort
    {
        private readonly ISendChatMessageUseCase _useCase;

        private IActionResult _viewModel;

        public ChatMessageController(ISendChatMessageUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(ChatMessage chatMessage)
        {
            _viewModel = Ok(chatMessage);
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendChatMessage(User sender, Room room, string text)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(sender, room, text);

            return _viewModel;
        }
    }
}
