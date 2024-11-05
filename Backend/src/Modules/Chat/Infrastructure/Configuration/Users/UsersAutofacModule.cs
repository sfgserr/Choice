using Autofac;
using Users.Application.Contracts;
using Users.Infrastructure;

namespace Chat.Infrastructure.Configuration.Users
{
    internal class UsersAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersModule>()
                .As<IUsersModule>()
                .InstancePerLifetimeScope();
        }
    }
}