using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Wallets.Commands.Deposit
{
    public class DepositCommand : ICommand
    {
        public DepositCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Guid PaymentId { get; }
    }
}