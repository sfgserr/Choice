using Choice.Domain.Models;

namespace Choice.Application.UseCases.Messages.SendChatMessage
{
    public class SendChatMessagePresenter : IOutputPort
    {
        public bool IsInvalid { get; set; } = false;
        public ChatMessage? Message { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(ChatMessage message)
        {
            Message = message;
        }
    }
}
