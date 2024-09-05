using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using Users.Application.Contracts;
using Users.Infrastructure.GeoCoding;

namespace Users.Infrastructure.Configuration.GeoCoding
{
    internal class GeoCodingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GeoService>()
                .As<IGeoService>()
                .SingleInstance()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
