
namespace Choice.Application.UseCases.ChatMessages.GetChatMessages
{
    public interface IGetChatMessagesUseCase
    {
        Task Execute(int user1Id, int user2Id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
