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
            Message message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

            message?.SetContent();

            return message;
        }

        public async Task<Message?> GetByOrderId(int orderId)
        {
            List<Message> messages = await _context.Messages.ToListAsync();

            messages.ForEach(m => m.SetContent());

            return messages.LastOrDefault(m => 
                m.Type == MessageType.Order && m.Content.Match("OrderId", orderId));
        }

        public async Task<Message?> GetByOrderRequestId(int orderRequestId)
        {
            List<Message> messages = await _context.Messages.ToListAsync();

            messages.ForEach(m => m.SetContent());

            return messages.FirstOrDefault(m =>
                m.Type == MessageType.Order && m.Content.Match("OrderRequestId", orderRequestId));
        }

        public async Task<IList<Message>> GetAll()
        {
            List<Message> messages = await _context.Messages.ToListAsync();

            messages.ForEach(m => m.SetContent());

            return messages;
        }
            
        public async Task<IList<Message>> GetAll(string senderId, string receiverId)
        {
            List<Message> messages = await _context.Messages.ToListAsync();

            messages.ForEach(m => m.SetContent());

            return messages.Where(m => (m.SenderId == senderId || m.SenderId == receiverId) 
                                            && (m.ReceiverId == receiverId || m.ReceiverId == senderId)).ToList();
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
