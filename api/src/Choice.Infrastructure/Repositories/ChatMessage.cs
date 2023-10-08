using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class ChatMessageRepository : IRepository<ChatMessage>
    {
        private readonly ChoiceContext _context;

        public ChatMessageRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> Create(ChatMessage entity)
        {
            await _context.ChatMessages.AddAsync(entity);

            return entity;
        }

        public async Task Delete(ChatMessage entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM ChatMessages WHERE ChatMessageId={entity.Id}");
        }

        public async Task<IList<ChatMessage>> Get()
        {
            return await _context.ChatMessages.ToListAsync();
        }

        public async Task<ChatMessage> GetBy(Func<ChatMessage, bool> func)
        {
            return await _context.ChatMessages.FirstOrDefaultAsync(c => func(c));
        }

        public async Task<ChatMessage> Update(ChatMessage entity)
        {
            await Task.Run(() =>
            {
                _context.ChatMessages.Update(entity);
            });

            return entity;
        }
    }
}
