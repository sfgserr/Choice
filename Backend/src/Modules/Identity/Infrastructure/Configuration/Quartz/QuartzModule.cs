using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using Quartz;

namespace Identity.Infrastructure.Configuration.Quartz
{
    internal class QuartzModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableFrom(typeof(IJob)))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}