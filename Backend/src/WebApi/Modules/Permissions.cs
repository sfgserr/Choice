namespace WebApi.Modules
{
    public static class Permissions
    {
        public const string GetClient = "GetClient";
        public const string ChangeClientData = "ChangeClientData";
        public const string ChangeClientIconUri = "ChangeClientIconUri";
        public const string ChangeCompanyData = "ChangeCompanyData";
        public const string ChangeCompanyIconUri = "ChangeCompanyIconUri";
        public const string FillData = "FillData";
        public const string BuySubscriptionPayment = "BuySubscriptionPayment";
        public const string PaySubscriptionPayment = "PaySubscriptionPayment";
        public const string PayEnrollmentPayment = "PayEnrollmentPayment";
        public const string GetSubscriptionPayment = "GetSubscriptionPayment";
        public const string GetCompany = "GetCompany";
        public const string GetCompanies = "GetCompanies";
        public const string GetCompanyOnMap = "GetCompanyOnMap";
        public const string CreateOrderRequest = "CreateOrderRequest";
        public const string ChangeOrderRequest = "ChangeOrderRequest";
        public const string GetOrderRequest = "GetOrderRequest";
        public const string GetOrderRequests = "GetOrderRequests";
        public const string GetOrderRequestsInRadius = "GetOrderRequestsInRadius";
        public const string Response = "Response";
        public const string Enroll = "Enroll";
        public const string Finish = "Finish";
	    public const string Cancel = "Cancel";
        public const string ChangeEnrollmentDate = "ChangeEnrollmentDate";
        public const string ConfirmEnrollmentDate = "ConfirmEnrollmentDate";
        public const string AddReview = "AddReview";
        public const string CreateMessage = "CreateMessage";
        public const string GetChat = "GetChat";
        public const string GetChats = "GetChats";
        public const string CreateCategory = "CreateCategory";
        public const string EditCategory = "EditCategory";
        public const string GetCategories = "GetCategories";
        public const string DeleteClient = "DeleteClient";
        public const string EditClient = "EditClient";
        public const string EditCompany = "EditCompany";
        public const string GetClientAdmin = "GetClientAdmin";
        public const string GetCompanyAdmin = "GetCompanyAdmin";
        public const string GetClients = "GetClients";
        public const string GetCompaniesAdmin = "GetCompaniesAdmin";
        public const string GetOrderRequestAsCompany = "GetOrderRequestAsCompany";
    }
}