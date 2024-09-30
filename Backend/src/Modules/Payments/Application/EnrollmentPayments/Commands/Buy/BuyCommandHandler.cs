using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;
using Payments.Domain.EnrollmentPayments;
using Payments.Domain.Payers;

namespace Payments.Application.EnrollmentPayments.Commands.Buy
{
    internal class BuyCommandHandler : ICommandHandler<BuyCommand>
    {
        private readonly IPaymentsDbContext _dbContext;
        private readonly IPayerContext _payerContext;
        
        internal BuyCommandHandler(IPaymentsDbContext dbContext, IPayerContext payerContext)
        {
            _dbContext = dbContext;
            _payerContext = payerContext;
        }

        public async Task Execute(BuyCommand command)
        {
            var enrollmentPayment = EnrollmentPayment.Buy(
                _payerContext.Id,
                new(command.ResponseId),
                new(command.Cost, command.Currency));

            await _dbContext.EnrollmentPayments.AddAsync(enrollmentPayment);
        }
    }
}