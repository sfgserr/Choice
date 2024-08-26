using BuildingBlocks.Infrastructure.Outbox;

namespace Users.Infrastructure.Data.Outbox
{
    internal class OutboxAccessor : IOutbox
    {
        private readonly UsersContext _usersContext;

        internal OutboxAccessor(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public void Add(OutboxMessage message)
        {
            _usersContext.OutboxMessages.Add(message);
        }
    }
}
