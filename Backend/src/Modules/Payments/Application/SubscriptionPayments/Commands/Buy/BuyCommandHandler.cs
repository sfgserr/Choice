using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;
using Payments.Domain.Payers;
using Payments.Domain.Subscriptions;
using Payments.Domain.SubscritpionPayments;

namespace Payments.Application.SubscriptionPayments.Commands.Buy
{
    internal class BuyCommandHandler : ICommandHandler<BuyCommand>
    {
        private readonly IPaymentsDbContext _dbContext;
        private readonly IPayerContext _payerContext;
        
        internal BuyCommandHandler(
            IPaymentsDbContext dbContext, 
            IPayerContext payerContext)
        {
            _dbContext = dbContext;
            _payerContext = payerContext;
        }

        public async Task Execute(BuyCommand command)
        {
            var subscriptionPayment = SubscriptionPayment.Buy(
                _payerContext.Id,
                SubscriptionPeriod.Parse(command.Period),
                _payerContext.Subscribed);

            await _dbContext.SubscriptionPayments.AddAsync(subscriptionPayment);
        }
    }
}