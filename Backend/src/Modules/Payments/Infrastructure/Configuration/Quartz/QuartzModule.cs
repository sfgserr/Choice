using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using Quartz;

namespace Payments.Infrastructure.Configuration.Quartz
{
    internal class QuartzModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => typeof(IJob).IsAssignableFrom(x))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
