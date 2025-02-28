using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.ExpressionTranslation;
using Microsoft.EntityFrameworkCore;
using Payments.Application.Contracts;
using Payments.Domain.SeedWork;
using Payments.Domain.SubscriptionPayments;

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
            var payment = await _dbContext.SubscriptionPayments.FirstOrDefaultAsync(
                ExpressionTranslator.Translate<SubscriptionPayment, SubscriptionPaymentDataModel, bool>(
                    s => s.Status.Equals(PaymentStatus.WaitingForPayment)));

            InvalidCommandException.ThrowIfNull(payment);
            
            payment!.Pay();
        }
    }
}