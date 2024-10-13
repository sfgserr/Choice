using IntegrationTests.Services;
using IntegrationTests.Services.Database;
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
            services.AddSingleton<WebApplicationFactory<Program>>();
            services.AddSingleton(s => new DbService(new(configuration["ConnectionString"]!)));
            services.AddSingleton(s => new AuthService(s.GetService<WebApplicationFactory<Program>>()!.CreateClient()));
        }

        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "dbsettings.json", IsOptional = false };
        }
    }
}