using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledWithPrepaymentIntegrationEvent : IntegrationEventBase
    {
        public EnrolledWithPrepaymentIntegrationEvent(
            Guid id,
            Guid responseId,
            double cost,
            string currency) : base(id)
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