using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;
using Payments.Domain.EnrollmentPayments;

namespace Payments.Application.EnrollmentPayments.Commands.Buy
{
    internal class BuyCommandHandler : ICommandHandler<BuyCommand>
    {
        private readonly IPaymentsDbContext _dbContext;

        internal BuyCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(BuyCommand command)
        {
            var enrollmentPayment = EnrollmentPayment.Buy(
                new(command.PayerId),
                new(command.ResponseId),
                new(command.Cost, command.Currency));

            await _dbContext.EnrollmentPayments.AddAsync(enrollmentPayment);
        }
    }
}