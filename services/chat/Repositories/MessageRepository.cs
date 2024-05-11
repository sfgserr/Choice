using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Infrastructure.Data;
using Choice.Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Choice.Chat.Api.Repositories
{
    public sealed class MessageRepository : IRepository<Message>
    {
        private readonly ChatDdContext _context;

        public MessageRepository(ChatDdContext context)
        {
            _context = context;
        }

        public async Task Add(Message message)
        {
            await _context.Messages.AddAsync(message);   
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Messages.Include(m => m.Receiver).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Message> GetByOrderId(int orderId)
        {
            return await _context.Messages.Include(m => m.Receiver).FirstOrDefaultAsync(m => m.Body.);
        }

        public async Task<IList<Message>> GetAll(string senderId, string receiverId)
        {
            return await _context.Messages.Include(m => m.Receiver)
                                          .Where(m => m.SenderId == senderId || m.SenderId == receiverId 
                                            && m.Receiver.Guid == receiverId || m.Receiver.Guid == senderId)
                                          .ToListAsync();
        }

        public void Update(Message message)
        {
            _context.Messages.Update(message);   
        }

        public async Task<bool> Delete(int id)
        {
            int affections = await _context.Database.ExecuteSqlRawAsync("DELETE FROM Messages WHERE Id = @p0", id);

            return affections > 0;
        }
    }
}
