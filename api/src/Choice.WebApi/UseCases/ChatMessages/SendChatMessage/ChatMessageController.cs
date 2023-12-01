using Choice.Application.UseCases.Messages.SendChatMessage;
using Choice.Domain.Models;
using Choice.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Choice.WebApi.UseCases.ChatMessages.SendChatMessage
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : Controller, IOutputPort
    {
        private readonly ISendChatMessageUseCase _useCase;
        private readonly IHubContext<ChatHub> _hubContext;

        private IActionResult _viewModel;

        public ChatMessageController(ISendChatMessageUseCase useCase)
        {
            _useCase = useCase;
        }

        async void IOutputPort.Ok(ChatMessage chatMessage)
        {
            _viewModel = Ok(chatMessage);
            await _hubContext.Clients.All.SendAsync("Receive", chatMessage.Room.Id, chatMessage.Text);
        }

        void IOutputPort.Invalid()
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendChatMessage(ChatMessage chatMessage)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(chatMessage.Sender, chatMessage.Room, chatMessage.Text);

            return _viewModel;
        }
    }
}
