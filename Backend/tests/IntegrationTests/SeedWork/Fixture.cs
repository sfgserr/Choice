using IntegrationTests.Services;
using IntegrationTests.Services.Database;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace IntegrationTests.SeedWork
{
    public class Fixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        {
            if (configuration is not null)
            {
                services.AddHttpClient("Default", o => o.BaseAddress = new("http://localhost:5271"));
                services.AddSingleton(s => new DbService(new(configuration["ConnectionString"]!)));
                services.AddSingleton<AuthService>();
            }
        }

        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}