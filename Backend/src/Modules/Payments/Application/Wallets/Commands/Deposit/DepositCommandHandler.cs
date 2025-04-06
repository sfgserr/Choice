using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Application.Services;
using Payments.Domain.Payers;

namespace Payments.Application.Wallets.Commands.Deposit
{
    internal class DepositCommandHandler : ICommandHandler<DepositCommand>
    {
        private readonly PaymentsService _paymentsService;
        private readonly IPaymentsDbContext _paymentsDbContext;
        
        internal DepositCommandHandler(PaymentsService paymentsService, IPaymentsDbContext paymentsDbContext)
        {
            _paymentsService = paymentsService;
            _paymentsDbContext = paymentsDbContext;
        }

        public async Task Execute(DepositCommand command)
        {
            var response = await _paymentsService.GetPaymentInfo(command.PaymentId);
            
            var wallet = await _paymentsDbContext.Wallets.Get(w => w.PayerId.Equals(new PayerId(response.PayerId)));
            
            wallet.Deposit(response.Copecks);
        }
    }
}