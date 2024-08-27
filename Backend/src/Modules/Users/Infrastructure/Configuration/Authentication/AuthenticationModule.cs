using Autofac;
using BuildingBlocks.Application.Authentication;
using BuildingBlocks.Infrastructure.Authentication;
using Users.Application.Users;
using Users.Domain.Users;

namespace Users.Infrastructure.Configuration.Authentication
{
    internal class AuthenticationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

            builder.Register(c => new UserContext(c.Resolve<IUserService>()))
                .As<IUserContext>()
                .InstancePerLifetimeScope();
        }
    }
}
