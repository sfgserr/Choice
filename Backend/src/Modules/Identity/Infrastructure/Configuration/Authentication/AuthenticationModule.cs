using Autofac;
using BuildingBlocks.Application.Authentication;
using Identity.Application.Authentication;
using Identity.Domain.Users;

namespace Identity.Infrastructure.Configuration.Authentication
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
            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .WithParameter("userService", _userService)
                .InstancePerLifetimeScope();
        }
    }
}