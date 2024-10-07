using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Contracts;
using Payments.Application.SubscriptionPayments.Commands.Buy;

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
        public async Task<IActionResult> Buy(string period)
        {
            await _module.ExecuteCommand(new BuyCommand(period));

            return Ok();
        }
    }
}
