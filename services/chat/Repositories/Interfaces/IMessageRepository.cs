using Choice.Chat.Api.Entities;

namespace Choice.Chat.Api.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message?> GetByOrderId(int orderId);

        Task<IList<Message>> GetAll(string senderId, string receiverId);

        Task<Message?> Get(int id);

        Task<bool> Delete(int id);
    }
}
