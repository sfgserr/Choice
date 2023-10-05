
namespace Choice.Application.UseCases.OrderMessages.GetOrderMessages
{
    public interface IGetOrderMessagesUseCase
    {
        Task Execute(int user1Id, int user2Id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
