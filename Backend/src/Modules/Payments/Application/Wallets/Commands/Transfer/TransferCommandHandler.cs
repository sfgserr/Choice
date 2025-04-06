using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Payments.Application.Contracts;
using Payments.Domain.Wallets;

namespace Payments.Application.Wallets.Commands.Transfer
{
    internal class TransferCommandHandler : ICommandHandler<TransferCommand>
    {
        private readonly IPaymentsDbContext _dbContext;

        internal TransferCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(TransferCommand command)
        {
            var fromWallet = await _dbContext.Wallets.Get(w => w.PayerId.Equals(new WalletId(command.FromUserId)));
            
            var toWallet = await _dbContext.Wallets.Get(w => w.PayerId.Equals(new WalletId(command.ToUserId)));
            
            fromWallet.Transfer(command.Copecks, toWallet);
        }
    }
}