using Choice.Domain.Models;

namespace Choice.Application.UseCases.Messages.SendChatMessage
{
    public interface ISendChatMessageUseCase
    {
        Task Execute(User sender, Room room, string text);

        void SetOutputPort(IOutputPort outputPort);
    }
}
