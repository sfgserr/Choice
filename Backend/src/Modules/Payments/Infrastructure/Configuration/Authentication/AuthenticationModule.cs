using Autofac;
using BuildingBlocks.Application.Authentication;
using Payments.Application.Subscriptions;
using Payments.Domain.Payers;

namespace Payments.Infrastructure.Configuration.Authentication
{
    internal class AuthenticationModule : Module
    {
        private readonly IUserService _userService;

        internal AuthenticationModule(IUserService userService)
        {
            _userService = userService;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new PayerContext(_userService))
                .As<IPayerContext>()
                .InstancePerLifetimeScope();
        }
    }
}
