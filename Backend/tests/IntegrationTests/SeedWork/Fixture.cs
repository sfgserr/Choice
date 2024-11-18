using IntegrationTests.Services.Auth;
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
                services.AddHttpClient("Default", o => o.BaseAddress = new(configuration["ServerUrl"]!));
                services.AddSingleton(s => new DbService(new(configuration["PostgreConnectionString"]!)));
                services.AddSingleton<AuthService>();
                services.Configure<AppOptions>(configuration.GetSection("App"));
            }
        }
        
        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "testsettings.json", IsOptional = false };
        }
    }
}