using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Middlewares.SubscriptionCheck;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Contracts;
using Payments.Application.Payments.CreatePayment;
using Payments.Application.Payouts.Commands.CreatePayout;
using Payments.Application.Wallets.Commands.Deposit;
using Payments.Application.Wallets.Commands.Withdraw;
using Payments.Application.Wallets.Queries.GetWallet;

namespace WebApi.Modules.Payments.Wallets
{
    [Route("api/wallets")]
    public class WalletController : Controller
    {
        private readonly IPaymentsModule _module;

        public WalletController(IPaymentsModule module)
        {
            _module = module;
        }

        [HasPermission(Permissions.CreatePayment)]
        [AllowUnsubscribe]
        [HttpPost("payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var confirmationUrl = await _module.ExecuteCommand<CreatePaymentCommand, string>(
                new(request.Copecks));
            
            return Ok(confirmationUrl);
        }
        
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] EventRequest request)
        {
            await _module.ExecuteCommand(new DepositCommand(Guid.Parse(request.Object.Id)));
            
            return Ok();
        }
        
        [HasPermission(Permissions.CreatePayout)]
        [AllowUnsubscribe]
        [HttpPost("payout")]
        public async Task<IActionResult> CreatePayout([FromBody] CreatePayoutRequest request)
        {
            await _module.ExecuteCommand(new CreatePayoutCommand(request.BankCardNumber, request.Copecks));
            
            return Ok();
        }
        
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] EventRequest request)
        {
            await _module.ExecuteCommand(new WithdrawCommand(request.Object.Id));

            return Ok();
        }
        
        [HasPermission(Permissions.GetWallet)]
        [AllowUnsubscribe]
        [HttpGet]
        public async Task<IActionResult> GetWallet()
        {
            var copecks = await _module.Query<GetWalletQuery, int>(new());
            
            return Ok(copecks);
        }
    }
}