using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.OrderResponses
{
    public interface IPaymentService
    {
        Task TransferMoney(ClientId clientId, CompanyId companyId, double amount);
    }
}