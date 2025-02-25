using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;

namespace Payments.Application.SubscriptionPayments.Commands.Expire
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
            var payments = _dbContext.SubscriptionPayments
                .Where(s => s.IsActive)
                .AsEnumerable();

            foreach (var payment in payments) payment.Expire();

            return Task.CompletedTask;
        }
    }
}
