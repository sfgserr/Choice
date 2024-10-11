using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace IntegrationTests.SeedWork
{
    public class Fixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            services.AddScoped<WebApplicationFactory<Program>>();
            services.AddSingleton(s => new DbOptions(configuration["ConnectionString"]!));
        }

        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "dbsettings.json", IsOptional = false };
        }
    }
}
