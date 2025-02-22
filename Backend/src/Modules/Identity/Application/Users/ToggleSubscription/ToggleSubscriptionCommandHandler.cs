using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Identity.Application.Contracts;
using Identity.Domain.Users;

namespace Identity.Application.Users.ToggleSubscription
{
    internal class ToggleSubscriptionCommandHandler : ICommandHandler<ToggleSubscriptionCommand>
    {
        private readonly IIdentityDbContext _dbContext;

        internal ToggleSubscriptionCommandHandler(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ToggleSubscriptionCommand command)
        {
            var user = await _dbContext.Users.Get(u => u.Id.Equals(new UserId(command.UserId)));
            
            user.ToggleSubscription();
        }
    }
}