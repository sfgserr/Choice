using Choice.Domain.Models;

namespace Choice.Application.UseCases.Messages.SendChatMessage
{
    public interface ISendChatMessageUseCase
    {
        Task Execute(User sender, User receiver, string text);

        void SetOutputPort(IOutputPort outputPort);
    }
}
