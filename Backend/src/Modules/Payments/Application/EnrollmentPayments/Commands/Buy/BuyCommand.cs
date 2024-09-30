using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.EnrollmentPayments.Commands.Buy
{
    public class BuyCommand : InternalCommandBase
    {
        public BuyCommand(Guid id, Guid responseId, double cost, string currency) : base(id)
        {
            ResponseId = responseId;
            Cost = cost;
            Currency = currency;
        }

        public Guid ResponseId { get; }
        
        public double Cost { get; }

        public string Currency { get; }
    }
}