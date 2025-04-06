using Payments.Application.Contracts.Dtos;

namespace Payments.Application.Contracts
{
    public interface IPaymentsGateway
    {
        Task<PaymentDto> ProcessPayment(Guid payerId, double amount);
        
        Task<PaymentDto?> Get(Guid paymentId);
        
        Task<PayoutDto> ProcessPayout(Guid payerId, string bankCardNumber, double amount);
        
        Task<PayoutDto?> GetPayout(string payoutId);
    }    
}