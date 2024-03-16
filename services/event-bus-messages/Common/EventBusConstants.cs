
namespace Choice.EventBus.Messages.Common
{
    public static class EventBusConstants
    {
        public const string OrderCreatedQueue = "ordering.order.created";
        public const string OrderChangedQueue = "ordering.order.changed";
        public const string UserDataChangedQueue = "auth-service.client.changed";
        public const string AuthorDataChangedQueue = "auth-service.author.changed";
        public const string OrderStatusChangedQueue = "ordering.order.status.changed";
        public const string ClientCreatedQueue = "auth-service.client.created";
        public const string CompanyCreatedQueue = "auth-service.company.created";
        public const string AuthorCreatedQueue = "auth-service.author.created";
        public const string ClientAverageGradeChangedQueue = "review-service.client.averagegrade.changed";
        public const string CompanyAverageGradeChangedQueue = "review-service.company.averagegrade.changed";
    }
}
