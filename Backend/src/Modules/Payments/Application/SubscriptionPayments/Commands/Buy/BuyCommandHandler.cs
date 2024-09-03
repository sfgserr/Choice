using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Domain.Payers;
using Payments.Domain.Subscriptions;
using Payments.Domain.SubscritpionPayments;

namespace Payments.Application.SubscriptionPayments.Commands.Buy
{
    internal class BuyCommandHandler : ICommandHandler<BuyCommand>
    {
        private readonly ISubscriptionPaymentRepository _repository;
        private readonly IPayerContext _payerContext;
        private readonly ISubscriptionsCounter _counter;
        
        internal BuyCommandHandler(
            ISubscriptionPaymentRepository repository, 
            IPayerContext payerContext, 
            ISubscriptionsCounter counter)
        {
            _repository = repository;
            _payerContext = payerContext;
            _counter = counter;
        }

        public async Task Execute(BuyCommand command)
        {
            var subscriptionPayment = SubscriptionPayment.Buy(
                _payerContext.Id,
                SubscriptionPeriod.Parse(command.Period),
                _counter);

            await _repository.Add(subscriptionPayment);
        }
    }
}