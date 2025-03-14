using Identity.Infrastructure.Authorization;
using Identity.Infrastructure.Middlewares.SubscriptionCheck;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Contracts;
using Payments.Application.SubscriptionPayments.Commands.Buy;
using Payments.Application.SubscriptionPayments.Commands.Pay;
using Payments.Application.SubscriptionPayments.Queries.GetPayment;

namespace WebApi.Modules.Payments.SubscriptionPayments
{
    [ApiController]
    [Route("api/subscriptionPayment")]
    public class SubscriptionPaymentController : Controller
    {
        private readonly IPaymentsModule _module;

        public SubscriptionPaymentController(IPaymentsModule module)
        {
            _module = module;
        }

        [HasPermission(Permissions.BuySubscriptionPayment)]
        [HttpPost("{period}")]
        [AllowUnsubscribe]
        public async Task<IActionResult> Buy(string period)
        {
            await _module.ExecuteCommand(new BuyCommand(period));

            return Ok();
        }

        [HasPermission(Permissions.PaySubscriptionPayment)]
        [HttpPut]
        [AllowUnsubscribe]
        public async Task<IActionResult> Pay()
        {
            await _module.ExecuteCommand(new PayCommand());

            return Ok();
        }

        [HasPermission(Permissions.GetSubscriptionPayment)]
        [HttpGet]
        [AllowUnsubscribe]
        public async Task<IActionResult> GetPayment()
        {
            var payment = await _module.Query<GetPaymentQuery, PaymentDto>(new GetPaymentQuery());

            return Ok(payment);
        }
    }
}
