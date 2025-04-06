using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Application.Services;
using Payments.Domain.Payers;

namespace Payments.Application.Payouts.Commands.CreatePayout
{
    internal class CreatePayoutCommandHandler : ICommandHandler<CreatePayoutCommand>
    {
        private readonly IPayerContext _payerContext;
        private readonly PayoutService _payoutService;
        private readonly IPaymentsDbContext _dbContext;
        
        internal CreatePayoutCommandHandler(IPayerContext payerContext, PayoutService payoutService, IPaymentsDbContext dbContext)
        {
            _payerContext = payerContext;
            _payoutService = payoutService;
            _dbContext = dbContext;
        }

        public async Task Execute(CreatePayoutCommand command)
        {
            var succeeded= await _payoutService.CreatePayout(_payerContext.Id.Value, command.BankCard, command.Copecks);

            if (succeeded)
            {
                var wallet = await _dbContext.Wallets.Get(w => w.PayerId.Equals(_payerContext.Id));
                
                wallet.Withdraw(command.Copecks);
            }
        }
    }
}