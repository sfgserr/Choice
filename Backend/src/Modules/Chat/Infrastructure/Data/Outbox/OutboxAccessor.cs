using BuildingBlocks.Infrastructure.Outbox;

namespace Chat.Infrastructure.Data.Outbox
{
    internal class OutboxAccessor : IOutbox
    {
        private readonly ChatContext _chatContext;

        internal OutboxAccessor(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public void Add(OutboxMessage message)
        {
            _chatContext.OutboxMessages.Add(message);
        }
    }
}