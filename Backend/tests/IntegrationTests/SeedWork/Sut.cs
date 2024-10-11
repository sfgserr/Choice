using System.Reflection;
using DbUp;
using DbUp.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace IntegrationTests.SeedWork
{
    public class Sut : TestBed<Fixture>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly DbOptions _dbOptions;

        public Sut(ITestOutputHelper outputHelper, Fixture testBed) : base(outputHelper, testBed)
        {
            _factory = testBed.GetService<WebApplicationFactory<Program>>(outputHelper)!;
            _dbOptions = testBed.GetService<DbOptions>(outputHelper)!;
        }

        protected async Task<bool> ExecuteTest(
            Func<WebApplicationFactory<Program>,Task<IProbe>> test)
        {
            var probe = await test(_factory);

            var result = probe.Test();

            await Task.Delay(20000);

            ClearDatabase();

            return result;
        }

        private void ClearDatabase()
        {
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(_dbOptions.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .JournalTo(new NullJournal())
                    .LogToConsole()
                    .Build();

            upgrader.PerformUpgrade();
        }
    }
}