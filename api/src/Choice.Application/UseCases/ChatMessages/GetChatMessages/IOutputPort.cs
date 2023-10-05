using Choice.Domain.Models;

namespace Choice.Application.UseCases.ChatMessages.GetChatMessages
{
    public interface IOutputPort
    {
        void Ok(IList<Message> chat);
    }
}
