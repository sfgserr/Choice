using Choice.Domain.Models;

namespace Choice.Application.UseCases.ChatMessages.GetChatMessages
{
    public class GetChatMessagesUseCasePresenter : IOutputPort
    {
        public IList<ChatMessage> Chat { get; set; }

        public void Ok(IList<ChatMessage> chat)
        {
            Chat = chat;
        }
    }
}
