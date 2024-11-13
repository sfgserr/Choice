using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledWithPrepaymentIntegrationEvent : IntegrationEventBase
    {
        public EnrolledWithPrepaymentIntegrationEvent(
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

        public Guid ResponseId { get; }
        
        public Guid ClientId { get; }

        public double Cost { get; }

        public string Currency { get; }
    }
}