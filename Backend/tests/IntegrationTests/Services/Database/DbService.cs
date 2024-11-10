using System.Reflection;
using Dapper;
using DbUp;
using DbUp.Helpers;
using Npgsql;

namespace IntegrationTests.Services.Database
{
    public class DbService
    {
        private readonly DbOptions _options;

        public DbService(DbOptions options)
        {
            _options = options;
        }

        public void ClearDatabase()
        {
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(_options.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .JournalTo(new NullJournal())
                    .LogToConsole()
                    .Build();

            upgrader.PerformUpgrade();
        }
    }
}
