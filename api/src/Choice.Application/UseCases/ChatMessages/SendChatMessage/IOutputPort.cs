using Choice.Domain.Models;

namespace Choice.Application.UseCases.Messages.SendChatMessage
{
    public interface IOutputPort
    {
        void Ok(ChatMessage message);

        void Invalid();
    }
}
