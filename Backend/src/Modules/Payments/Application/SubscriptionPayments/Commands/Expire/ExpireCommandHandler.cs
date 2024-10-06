using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;
using Payments.Domain.SeedWork;

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
                .Where(s => s.Status.Equals(PaymentStatus.WaitingForPayment))
                .AsEnumerable();

            foreach (var payment in payments) 
            {
                if (payment.ExpirationDate > DateTime.UtcNow)
                    payment.Expire();
            }

            return Task.CompletedTask;
        }
    }
}
