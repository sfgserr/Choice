using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.EnrollmentPayments.Commands.Buy
{
    public class BuyCommand : InternalCommandBase
    {
        public BuyCommand(Guid id, Guid payerId, Guid responseId, double cost, string currency) : base(id)
        {
            PayerId = payerId;
            ResponseId = responseId;
            Cost = cost;
            Currency = currency;
        }

        public Guid PayerId { get; }

        public Guid ResponseId { get; }
        
        public double Cost { get; }

        public string Currency { get; }
    }
}