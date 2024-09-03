using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.SubscriptionPayments.Commands.Buy
{
    public class BuyCommand : ICommand
    {
        public BuyCommand(string period)
        {
            Period = period;
        }

        public string Period { get; }
    }
}