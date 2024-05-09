using Dapper;
using Npgsql;
using Polly;

namespace Choice.Chat.Api.Extensions
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

            string messagesTableName = "Messages";
            bool messagesExists = connection.QueryFirstOrDefault<bool>(
                "SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = @TableName)", new { TableName = messagesTableName });

            string ordersTableName = "Orders";
            bool ordersExists = connection.QueryFirstOrDefault<bool>(
                "SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = @TableName)", new { TableName = ordersTableName });

            if (!messagesExists)
            {
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

            if (!ordersExists)
            {
                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = @"CREATE TABLE Orders(Id SERIAL PRIMARY KEY,
                                                            OrderId INTEGER NOT NULL,
                                                            Prepayment INTEGERR NOT NULL,
                                                            Deadline INTEGER NOT NULL,
                                                            Price INTEGER NOT NULL,
                                                            SenderId VARCHAR(24) NOT NULL,
                                                            ReceiverId VARCHAR(24) NOT NULL,
                                                            CreationTime TIMESTAMP NOT NULL,
                                                            EnrollmentTime TIMESTAMP NOT NULL,
                                                            IsEnrolled BOOLEAN NOT NULL)";

                command.ExecuteNonQuery();
            }
        }
    }
}
