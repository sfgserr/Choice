using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Infrastructure.Data;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Repositories
{
    public sealed class MessageRepository : IMessageRepository
    {
        private readonly ChatDdContext _context;

        public MessageRepository(ChatDdContext context)
        {
            _context = context;
        }

        public async Task Add(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message?> Get(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Message?> GetByOrderId(int orderId)
        {
            IList<Message> messages = await _context.Messages.ToListAsync();

            return messages.LastOrDefault(m => 
                m.Type == MessageType.Order && JsonConvert.DeserializeObject<Order>(m.Body).OrderId == orderId);
        }

        public async Task<Message?> GetByOrderRequestId(int orderRequestId)
        {
            return await _context.Messages.FirstOrDefaultAsync(m =>
                m.Type == MessageType.Order && JsonConvert.DeserializeObject<Order>(m.Body).OrderRequestId == orderRequestId);
        }

        public async Task<IList<Message>> GetAll() => 
            await _context.Messages.ToListAsync();

        public async Task<IList<Message>> GetAll(string senderId, string receiverId)
        {
            return await _context.Messages.Where(m => (m.SenderId == senderId || m.SenderId == receiverId) 
                                            && (m.ReceiverId == receiverId || m.ReceiverId == senderId))
                                          .ToListAsync();
        }

        public async Task Update(Message message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            int affections = await _context.Database.ExecuteSqlRawAsync("DELETE FROM Messages WHERE Id = @p0", id);

            return affections > 0;
        }
    }
}
