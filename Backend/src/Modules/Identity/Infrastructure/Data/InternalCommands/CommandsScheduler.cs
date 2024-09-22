using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.Serialization;
using Dapper;
using Newtonsoft.Json;

namespace Identity.Infrastructure.Data.InternalCommands
{
    internal class CommandsScheduler
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal CommandsScheduler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task EnqueueAsync(InternalCommandBase command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                INSERT INTO identity."InternalCommands" ("Id", "Type", "Data") VALUES (@Id, @Type, @Data); 
                """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    command.Id,
                    Type = command.GetType().FullName,
                    Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings()
                    {
                        ContractResolver = new AllPropertiesContractResolver()
                    })
                });
        }
    }
}