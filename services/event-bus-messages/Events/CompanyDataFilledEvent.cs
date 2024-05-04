
namespace Choice.EventBus.Messages.Events
{
    public class CompanyDataFilledEvent : IntegrationEvent
    {
        public CompanyDataFilledEvent(string companyGuid)
        {
            CompanyGuid = companyGuid;
        }

        public string CompanyGuid { get; }
    }
}
