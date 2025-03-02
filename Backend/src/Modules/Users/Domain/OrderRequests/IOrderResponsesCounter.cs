using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests
{
    public interface IOrderResponsesCounter
    {
        int Count(OrderRequestId requestId, CompanyId companyId);
    }
}