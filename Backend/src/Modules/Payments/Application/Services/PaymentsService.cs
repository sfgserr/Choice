using BuildingBlocks.Application.Exceptions;
using Payments.Application.Contracts;

namespace Payments.Application.Services
{
    public class PaymentsService
    {
        private readonly IPaymentsGateway _paymentsGateway;

        internal PaymentsService(IPaymentsGateway paymentsGateway)
        {
            _paymentsGateway = paymentsGateway;
        }

        public async Task<string> CreatePayment(Guid payerId, int copecks)
        {
            var response = await _paymentsGateway.ProcessPayment(
                payerId, 
                (double)copecks / 100);

            return response.Confirmation.ConfirmationUrl;
        }

        public async Task<GetPaymentInfoResponse> GetPaymentInfo(Guid paymentId)
        {
            var payment = await _paymentsGateway.Get(paymentId);
            InvalidCommandException.ThrowIfNull(payment);
            
            return new GetPaymentInfoResponse(
                Guid.Parse(payment.Metadata.First(c => c.Key == "payerId").Value),
                (int)(payment.Amount.Value * 100));
        }
    }

    public class GetPaymentInfoResponse
    {
        public GetPaymentInfoResponse(Guid payerId, int copecks)
        {
            PayerId = payerId;
            Copecks = copecks;
        }

        public Guid PayerId { get; }
        
        public int Copecks { get; }
    }
}