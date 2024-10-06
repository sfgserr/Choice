using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Subscriptions.Commands.Expire
{
    public class ExpireCommand : InternalCommandBase
    {
        public ExpireCommand(Guid id) : base(id)
        {
        }
    }
}
