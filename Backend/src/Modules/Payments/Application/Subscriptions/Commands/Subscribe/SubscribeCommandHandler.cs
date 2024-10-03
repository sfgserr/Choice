using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;
using Payments.Domain.Subscriptions;

namespace Payments.Application.Subscriptions.Commands.Subscribe
{
    internal class SubscribeCommandHandler : ICommandHandler<SubscribeCommand>
    {
        private readonly IPaymentsDbContext _dbContext;

        internal SubscribeCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(SubscribeCommand command)
        {
            var subscription = Subscription.Create(
                new(command.SubscriberId),
                SubscriptionPeriod.Parse(command.Period));

            await _dbContext.Subscriptions.AddAsync(subscription);
        }
    }
}