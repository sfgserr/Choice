using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Payouts.Commands.CreatePayout
{
    public class CreatePayoutCommand : ICommand
    {
        public CreatePayoutCommand(string bankCard, int copecks)
        {
            BankCard = bankCard;
            Copecks = copecks;
        }

        public string BankCard { get; }
        
        public int Copecks { get; }
    }
}