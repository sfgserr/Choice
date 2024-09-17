using Autofac;
using Identity.Application.Contracts;
using Identity.Infrastructure;

namespace WebApi.Modules.Identity
{
    public class IdentityAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityModule>()
                .As<IIdentityModule>()
                .InstancePerLifetimeScope();
        }
    }
}