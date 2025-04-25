using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Wallets.Commands.CreateWallet 
{
    public class CreateWalletCommand : InternalCommandBase
    {
        public CreateWalletCommand(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }
        
        public Guid UserId { get; }
    }
}