using BuildingBlocks.Infrastructure.Outbox;

namespace Payments.Infrastructure.Data.Outbox
{
    internal class OutboxAccessor : IOutbox
    {
        private readonly PaymentsContext _usersContext;

        internal OutboxAccessor(PaymentsContext usersContext)
        {
            _usersContext = usersContext;
        }

        public void Add(OutboxMessage message)
        {
            _usersContext.OutboxMessages.Add(message);
        }
    }
}
