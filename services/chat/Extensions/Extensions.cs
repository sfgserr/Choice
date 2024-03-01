using Npgsql;
using Polly;

namespace Chat.Extensions
{
    public static class Extensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();

            var retry = Policy.Handle<NpgsqlException>()
                          .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            retry.Execute(() => ExecuteMigration(configuration));

            return host;
        }

        private static void ExecuteMigration(IConfiguration configuration)
        {
            using var connection = new NpgsqlConnection(configuration["PostgreSqlSettings:ConnectionString"]);
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = @"CREATE TABLE Messages(Id SERIAL PRIMARY KEY, 
                                                                SenderId VARCHAR(24) NOT NULL,
                                                                ReceiverId VARCHAR(24) NOT NULL,
                                                                Text VARCHAR(24) NOT NULL)";
            command.ExecuteNonQuery();
        }
    }
}
