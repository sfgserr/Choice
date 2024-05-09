
namespace Choice.EventBus.Messages.Common
{
    public static class EventBusConstants
    {
        public const string OrderCreatedQueue = "ordering.order.created";
        public const string UserEnrolledQueue = "ordering.order.enrolled";
        public const string OrderEnrollmentDateChangedQueue = "ordering.order.date.changed";
        public const string UserDataChangedQueue = "auth-service.user.changed";
        public const string AuthorDataChangedQueue = "auth-service.author.changed";
        public const string AuthorIconUriChangedQueue = "auth-service.author.icon.changed";
        public const string OrderStatusChangedQueue = "ordering.order.status.changed";
        public const string OrderMessageStatusChangedQueue = "ordering.message.status.changed";
        public const string ClientCreatedQueue = "auth-service.client.created";
        public const string CompanyCreatedQueue = "auth-service.company.created";
        public const string AuthorCreatedQueue = "auth-service.author.created";
        public const string ClientAverageGradeChangedQueue = "review-service.client.averagegrade.changed";
        public const string CompanyAverageGradeChangedQueue = "review-service.company.averagegrade.changed";
        public const string CompanyDataFilledQueue = "company-service.user.isdatafilled.changed";
    }
}
