using BuildingBlocks.Application.Cqrs.Commands;
using Microsoft.EntityFrameworkCore;
using Payments.Application.Contracts;

namespace Payments.Application.Subscriptions.Commands.Expire
{
    internal class ExpireCommandHandler : ICommandHandler<ExpireCommand>
    {
        private readonly IPaymentsDbContext _dbContext;

        internal ExpireCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ExpireCommand command)
        {
            var subscriptions = await _dbContext.Subscriptions.ToListAsync();

            foreach (var subscription in subscriptions) 
            {
                if (subscription.ExpirationDate > DateTime.UtcNow)
                    subscription.Expire();
            }
        }
    }
}
