using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class EnrolledIntegrationEvent : IntegrationEventBase
    {
        public EnrolledIntegrationEvent(Guid id, Guid responseId, Guid companyId) : base(id)
        {
            ResponseId = responseId;
            CompanyId = companyId;
        }

        public Guid ResponseId { get; }
        
        public Guid CompanyId { get; }
    }
}
