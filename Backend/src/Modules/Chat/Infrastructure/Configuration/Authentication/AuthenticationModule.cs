using Autofac;
using BuildingBlocks.Application.Authentication;
using Chat.Domain.ChatUsers;
using Chat.Application.ChatUsers;

namespace Chat.Infrastructure.Configuration.Authentication
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