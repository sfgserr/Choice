using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.EnrollmentPayments.Commands.Buy
{
    public class BuyCommand : InternalCommandBase
    {
        public BuyCommand(
            Guid id, 
            Guid responseId,
            Guid clientId,
            double cost, 
            string currency) : base(id)
        {
            ResponseId = responseId;
            ClientId = clientId;
            Cost = cost;
            Currency = currency;
        }

        internal Guid ResponseId { get; }
        
        internal Guid ClientId { get; }
        
        internal double Cost { get; }

        internal string Currency { get; }
    }
}