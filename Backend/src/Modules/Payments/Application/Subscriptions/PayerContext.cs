using BuildingBlocks.Application.Authentication;
using Payments.Domain.Payers;

namespace Payments.Application.Subscriptions
{
    public class PayerContext : IPayerContext
    {
        private readonly IUserService _userService;

        public PayerContext(IUserService userService)
        {
            _userService = userService;
        }

        public PayerId Id => new(_userService.GetUserId());
    }
}
