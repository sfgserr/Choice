using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Subscriptions.Commands.Subscribe
{
    public class SubscribeCommand : InternalCommandBase
    {
        public SubscribeCommand(Guid id, Guid subscriberId, string period) : base(id)
        {
            SubscriberId = subscriberId;
            Period = period;
        }
        
        internal Guid SubscriberId { get; }
        
        internal string Period { get; }
    }
}