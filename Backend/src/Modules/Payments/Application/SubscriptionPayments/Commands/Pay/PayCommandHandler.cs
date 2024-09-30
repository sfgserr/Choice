using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Domain.SubscritpionPayments;

namespace Payments.Application.SubscriptionPayments.Commands.Pay
{
    internal class PayCommandHandler : ICommandHandler<PayCommand>
    {
        private readonly IPaymentsDbContext _dbContext;
        
        internal PayCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(PayCommand command)
        {
            var payment = await _dbContext.SubscriptionPayments.Get(c => 
                c.Id.Equals(new SubscrtipionPaymentId(command.PaymentId)));
            
            payment.Pay();
        }
    }
}