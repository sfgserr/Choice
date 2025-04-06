using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Application.Services;
using Payments.Domain.Payers;

namespace Payments.Application.Wallets.Commands.Withdraw
{
    internal class WithdrawCommandHandler : ICommandHandler<WithdrawCommand>
    {
        private readonly PayoutService _payoutService;
        private readonly IPaymentsDbContext _dbContext;
        
        internal WithdrawCommandHandler(PayoutService payoutService, IPaymentsDbContext dbContext)
        {
            _payoutService = payoutService;
            _dbContext = dbContext;
        }

        public async Task Execute(WithdrawCommand command)
        {
            var response = await _payoutService.GetPayoutInfo(command.PayOutId);

            var wallet = await _dbContext.Wallets.Get(w => w.PayerId.Equals(new PayerId(response.PayerId)));
            
            wallet.Withdraw(response.Copecks);
        }
    }
}