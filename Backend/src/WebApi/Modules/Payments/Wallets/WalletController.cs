using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Middlewares.SubscriptionCheck;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Contracts;
using Payments.Application.Payments.CreatePayment;
using Payments.Application.Payouts.Commands.CreatePayout;
using Payments.Application.Wallets.Queries.GetWallet;
using Payments.Infrastructure.YooKassa.Events.Core;

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
            var result = await _module.ExecuteCommand<CreatePaymentCommand, CreatePaymentResult>(
                new(request.Copecks));
            
            return Ok(result);
        }
        
        [HttpPost("notification")]
        public async Task<IActionResult> Deposit([FromBody] HttpRequest request)
        {
            string body = "";
            using (StreamReader stream = new StreamReader(request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            
            await YooKassaNotifications.Handle(body);
            
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