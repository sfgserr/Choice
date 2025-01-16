using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Payments.Application.Contracts;

namespace Payments.Application.SubscriptionPayments.Commands.Pay
{
    internal class PayCommandHandler : ICommandHandler<PayCommand>
    {
        private readonly IPaymentsDbContext _dbContext;
        
        internal PayCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Execute(PayCommand command)
        {
            var payment = _dbContext.SubscriptionPayments.AsEnumerable().FirstOrDefault(c => c.Status.Value.Equals("WaitingForPayment"));

            InvalidCommandException.ThrowIfNull(payment);
            
            payment.Pay();

            return Task.CompletedTask;
        }
    }
}