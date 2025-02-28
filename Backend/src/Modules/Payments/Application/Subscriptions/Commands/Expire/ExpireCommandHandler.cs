using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Domain.Subscriptions;

namespace Payments.Application.Subscriptions.Commands.Expire
{
    internal class ExpireCommandHandler : ICommandHandler<ExpireCommand>
    {
        private readonly IPaymentsDbContext _dbContext;

        internal ExpireCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Execute(ExpireCommand command)
        {
            var subscriptions = _dbContext.Subscriptions
                .TranslatedWhere<Subscription, SubscriptionDataModel>(s => s.Status.Equals(SubscriptionStatus.Active));

            foreach (var subscription in subscriptions) subscription.Expire();

            return Task.CompletedTask;
        }
    }
}
