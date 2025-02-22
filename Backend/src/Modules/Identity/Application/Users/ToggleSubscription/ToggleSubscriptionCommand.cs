using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Users.ToggleSubscription
{
    public class ToggleSubscriptionCommand : InternalCommandBase
    {
        public ToggleSubscriptionCommand(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}