using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Contracts;
using Payments.Domain.Wallets;

namespace Payments.Application.Wallets.Commands.CreateWallet
{
    internal class CreateWalletCommandHandler : ICommandHandler<CreateWalletCommand>
    {
        private readonly IPaymentsDbContext _dbContext;

        internal CreateWalletCommandHandler(IPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(CreateWalletCommand command)
        {
            var wallet = Wallet.Create(new(command.UserId));
            
            await _dbContext.Wallets.AddAsync(wallet);
        }
    }
}