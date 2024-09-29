using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Domain.EnrollmentPayments;

namespace Payments.Application.EnrollmentPayments.Commands.Pay
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
            var payment = await _dbContext.EnrollmentPayments.Get(p => 
                p.Id.Equals(new EnrollmentPaymentId(command.PaymentId)));
            
            payment.Pay();
        }
    }
}