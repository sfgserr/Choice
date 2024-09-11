using Autofac;
using BuildingBlocks.Application.Authentication;
using Users.Application.Users;
using Users.Domain.Users;

namespace Users.Infrastructure.Configuration.Authentication
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
            builder.Register(c => new UserContext(_userService))
                .As<IUserContext>()
                .InstancePerLifetimeScope();
        }
    }
}
