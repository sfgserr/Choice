using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Wallets.Commands.Withdraw
{
    public class WithdrawCommand : ICommand
    {
        public WithdrawCommand(string payOutId)
        {
            PayOutId = payOutId;
        }

        public string PayOutId { get; }
    }
}