using Administration.Application.Contracts;
using Administration.Infrastructure;
using Autofac;

namespace WebApi.Modules.Admin
{
    public class AdminAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdministrationModule>()
                .As<IAdministrationModule>()
                .InstancePerLifetimeScope();
        }
    }
}