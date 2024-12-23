using Autofac;
using BuildingBlocks.Infrastructure.Configuration;
using Users.Application.Contracts;
using Users.Infrastructure.GeoCoding;

namespace Users.Infrastructure.Configuration.GeoCoding
{
    internal class GeoCodingModule : Module
    {
        private readonly IHttpClientFactory _factory;
        private readonly string _apiKey;
        
        internal GeoCodingModule(IHttpClientFactory factory, string apiKey)
        {
            _factory = factory;
            _apiKey = apiKey;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_factory)
                .As<IHttpClientFactory>()
                .SingleInstance();
            
            builder.Register(s => new GeoService(s.Resolve<IHttpClientFactory>(), _apiKey))
                .As<IGeoService>()
                .InstancePerDependency();
        }
    }
}
