using Autofac;
using BuildingBlocks.Application.GeoCoding;
using BuildingBlocks.Infrastructure.GeoCoding;

namespace Users.Infrastructure.Configuration.GeoCoding
{
    internal class GeoCodingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GeoService>()
                .As<IGeoService>()
                .SingleInstance();
        }
    }
}
