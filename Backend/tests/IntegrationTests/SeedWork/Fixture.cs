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
                services.AddSingleton<DbService>();
                services.AddSingleton<AuthService>();
                services.Configure<AppOptions>(configuration.GetSection("App"));
                services.Configure<DbOptions>(configuration.GetSection("PostgreSqlSettings"));
            }
        }
        
        protected override ValueTask DisposeAsyncCore() => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "testsettings.json", IsOptional = false };
        }
    }
}