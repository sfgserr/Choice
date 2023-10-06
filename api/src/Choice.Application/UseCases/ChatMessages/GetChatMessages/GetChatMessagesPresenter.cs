using Choice.Domain.Models;

namespace Choice.Application.UseCases.ChatMessages.GetChatMessages
{
    public class GetChatMessagesPresenter : IOutputPort
    {
        public IList<ChatMessage> Chat { get; set; } = new List<ChatMessage>();

        public void Ok(IList<ChatMessage> chat)
        {
            Chat = chat;
        }
    }
}
