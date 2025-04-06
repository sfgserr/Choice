using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Wallets.Commands.Transfer
{
    public class TransferCommand : ICommand
    {
        public TransferCommand(Guid toUserId, Guid fromUserId, int copecks)
        {
            ToUserId = toUserId;
            FromUserId = fromUserId;
            Copecks = copecks;
        }

        public Guid ToUserId { get; }

        public Guid FromUserId { get; }

        public int Copecks { get; }
    }
}