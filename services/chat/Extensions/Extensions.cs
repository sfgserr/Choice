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

            //retry.Execute(() => ExecuteMigration(configuration));

            ExecuteMigration(configuration);

            return host;
        }

        private static void ExecuteMigration(IConfiguration configuration)
        {
            using var connection = new NpgsqlConnection(configuration["PostgreSqlSettings:ConnectionString"]);
            connection.Open();

            using var messagesTableCreationCommand = new NpgsqlCommand
            {
                Connection = connection
            };

            messagesTableCreationCommand.CommandText = @"CREATE TABLE Messages(Id SERIAL PRIMARY KEY, 
                                                                SenderId VARCHAR(24) NOT NULL,
                                                                GroupId VARCHAR(24) NOT NULL,
                                                                Text VARCHAR(24) NOT NULL)";

            messagesTableCreationCommand.ExecuteNonQuery();

            using var ordersTableCreationCommand = new NpgsqlCommand
            {
                Connection = connection
            };

            ordersTableCreationCommand.CommandText = @"CREATE TABLE Orders(Id SERIAL PRIMARY KEY,
                                                            OrderId INTEGER NOT NULL,
                                                            Prepayment INTEGER NOT NULL,
                                                            Deadline INTEGER NOT NULL,
                                                            Price INTEGER NOT NULL,
                                                            SenderId VARCHAR(24) NOT NULL,
                                                            ReceiverId VARCHAR(24) NOT NULL,
                                                            CreationTime TIMESTAMP NOT NULL,
                                                            EnrollmentDate TIMESTAMP NOT NULL,
                                                            IsEnrolled BOOLEAN NOT NULL,
                                                            DateChanged BOOLEAN NOT NULL,
                                                            Status INTEGER NOT NULL)";

            ordersTableCreationCommand.ExecuteNonQuery();


            using var groupsTableCreationCommand = new NpgsqlCommand
            {
                Connection = connection
            };

            groupsTableCreationCommand.CommandText = @"CREATE TABLE Groups(Id SERIAL PRIMARY KEY,
                                                                           UserGuid VARCHAR(24) NOT NULL, 
                                                                           SenderId VARCHAR(24) NOT NULL)";

            groupsTableCreationCommand.ExecuteNonQuery();
        }
    }
}
