using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Contracts;
using Payments.Application.EnrollmentPayments.Commands.Pay;
using WebApi.Configuration.Authorization;

namespace WebApi.Modules.Payments.EnrollmentPayments
{
    [ApiController]
    [Route("api/enrollmentPayments")]
    public class EnrollmentPaymentController : Controller
    {
        private readonly IPaymentsModule _module;

        public EnrollmentPaymentController(IPaymentsModule module)
        {
            _module = module;
        }

        [HasPermission(Permissions.PayEnrollmentPayment)]
        [HttpPut("{responseId:guid}")]
        public async Task<IActionResult> Pay(Guid responseId)
        {
            await _module.ExecuteCommand(new PayCommand(responseId));

            return Ok();
        }
    }
}
