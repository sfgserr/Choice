using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class ChatMessageRepositoryFake : IRepository<ChatMessage>
    {
        private readonly ChoiceContextFake _context;

        public ChatMessageRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<ChatMessage> Create(ChatMessage entity)
        {
            _context.ChatMessages.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(ChatMessage entity)
        {
            ChatMessage entityToRemove = _context.ChatMessages.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.ChatMessages.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<ChatMessage>> Get()
        {
            IList<ChatMessage> chatMessages = _context.ChatMessages.ToList();

            return await Task.FromResult(chatMessages);
        }

        public async Task<ChatMessage> GetBy(Func<ChatMessage, bool> func)
        {
            ChatMessage chatMessage = _context.ChatMessages.FirstOrDefault(c => func(c));

            return await Task.FromResult(chatMessage);
        }

        public async Task<ChatMessage> Update(ChatMessage entity)
        {
            ChatMessage oldChatMessage = _context.ChatMessages.FirstOrDefault(c => c.Id == entity.Id);

            if (oldChatMessage != null)
            {
                _context.ChatMessages.Remove(oldChatMessage);
            }

            _context.ChatMessages.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
