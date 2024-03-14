
namespace Choice.EventBus.Messages.Events
{
    public class ReviewLeftEvent : IntegrationEvent
    {
        public ReviewLeftEvent(string userGuid, int grade)
        {
            UserGuid = userGuid;
            Grade = grade;
        }

        public string UserGuid { get; }
        public int Grade { get; }
    }
}
