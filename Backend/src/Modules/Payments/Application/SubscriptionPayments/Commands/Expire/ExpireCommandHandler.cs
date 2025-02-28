using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Domain.SeedWork;
using Payments.Domain.SubscriptionPayments;

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
                .TranslatedWhere<SubscriptionPayment, SubscriptionPaymentDataModel>(s => s.Status.Equals(PaymentStatus.WaitingForPayment))
                .AsEnumerable();

            foreach (var payment in payments) payment.Expire();

            return Task.CompletedTask;
        }
    }
}
