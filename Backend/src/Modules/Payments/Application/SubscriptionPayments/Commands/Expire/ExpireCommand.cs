using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.SubscriptionPayments.Commands.Expire
{
    public class ExpireCommand : InternalCommandBase
    {
        public ExpireCommand(Guid id) : base(id)
        {
        }
    }
}
