using DatabaseMigrator.Filters;
using DbUp;
using DbUp.ScriptProviders;
using Serilog;

namespace DatabaseMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logger.Information("Logger configured. Starting migration");

            if (args.Length < 2 || !Directory.Exists(args[1]))
            {
                logger.Error("Files don't exist");
                return;
            }
        
            string connectionString = args[0];

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsFromFileSystem(args[1], new FileSystemScriptOptions() 
                {
                    IncludeSubDirectories = true
                })
                .WithFilter(new SqlScriptsFilter())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                logger.Error("Migration failed with error {Error}", result.Error.Message);
                return;
            }

            logger.Information("Database has been migrated successfully");
        }
    }
}
