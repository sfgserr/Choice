
namespace Choice.EventBus.Messages.Common
{
    public static class EventBusConstants
    {
        public const string OrderCreatedQueue = "ordering.order.created";
        public const string OrderChangedQueue = "ordering.order.changed";
        public const string UserDataChangedQueue = "auth-service.user.changed";
        public const string OrderStatusChangedQueue = "ordering.order.status.changed";
        public const string UserCreatedQueue = "auth-service.user.created";
        public const string AuthorCreatedQueue = "auth-service.author.created";
        public const string ReviewAddedToOrderQueue = "review-service.order.review.added";
        public const string AverageGradeChangedQueue = "review-service.client.averagegrade.changed";
    }
}
