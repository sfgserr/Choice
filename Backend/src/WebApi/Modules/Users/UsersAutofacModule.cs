using Autofac;
using Users.Application.Contracts;
using Users.Infrastructure;

namespace WebApi.Modules.Users
{
    public class UsersAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersModule>()
                .As<IUsersModule>()
                .InstancePerLifetimeScope();
        }
    }
}
