using Payments.Application.Contracts;
using Payments.Application.Wallets.Commands.Transfer;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Infrastructure.Payments
{
    internal class PaymentService : IPaymentService
    {
        private readonly IPaymentsModule _paymentsModule;

        internal PaymentService(IPaymentsModule paymentsModule)
        {
            _paymentsModule = paymentsModule;
        }

        public async Task TransferMoney(ClientId clientId, CompanyId companyId, double amount)
        {
            await _paymentsModule.ExecuteCommand(new TransferCommand(
                companyId.Value, 
                clientId.Value,
                (int)(amount * 100)));
        }
    }
}