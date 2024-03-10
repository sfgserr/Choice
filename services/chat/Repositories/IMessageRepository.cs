using Choice.Chat.Api.Entities;

namespace Choice.Chat.Api.Repositories
{
    public interface IMessageRepository
    {
        Task Add(Message message);

        Task<Message> Get(int id, string senderId);

        Task<IList<Message>> GetAll(string senderId, string receiverId);

        Task<bool> Update(Message message);

        Task<bool> Delete(int id);
    }
}
