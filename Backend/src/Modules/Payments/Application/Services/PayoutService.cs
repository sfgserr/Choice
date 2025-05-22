using BuildingBlocks.Application.Exceptions;
using Payments.Application.Contracts;

namespace Payments.Application.Services
{
    public class PayoutService
    {
        private readonly IPaymentsGateway _paymentsGateway;

        internal PayoutService(IPaymentsGateway paymentsGateway)
        {
            _paymentsGateway = paymentsGateway;
        }

        public async Task<bool> CreatePayout(Guid receiverId, string bankCardNumber, int copecks)
        {
            var payout = await _paymentsGateway.ProcessPayout(receiverId, bankCardNumber, (double)copecks / 100);

            return payout.Status == "success";
        }

        public async Task<GetPayoutInfoResponse> GetPayoutInfo(string payoutId)
        {
            var payout = await _paymentsGateway.GetPayout(payoutId);
            InvalidCommandException.ThrowIfNull(payout);
            
            return new GetPayoutInfoResponse(
                Guid.Parse(payout.Metadata.First(x => x.Key == "payerId").Value),
                (int)(payout.Amount.Value * 100));
        }
    }
    
    public class GetPayoutInfoResponse
    {
        public GetPayoutInfoResponse(Guid payerId, int copecks)
        {
            PayerId = payerId;
            Copecks = copecks;
        }

        public Guid PayerId { get; }
        
        public int Copecks { get; }
    }
}