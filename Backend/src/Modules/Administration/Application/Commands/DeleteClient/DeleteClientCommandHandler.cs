using Administration.IntegrationEvents;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Application.Exceptions;
using Dapper;

namespace Administration.Application.Commands.DeleteClient
{
    internal class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IEventBus _eventBus;
        
        internal DeleteClientCommandHandler(ISqlConnectionFactory connectionFactory, IEventBus eventBus)
        {
            _connectionFactory = connectionFactory;
            _eventBus = eventBus;
        }

        public async Task Execute(DeleteClientCommand command)
        {
            using var connection = _connectionFactory.GetConnection();

            const string deleteClientSql = "DELETE FROM users.\"Clients\" WHERE users.\"Clients\".\"Id\" = @Id;";

            const string deleteUserSql = "DELETE FROM users.\"Clients\" WHERE users.\"Clients\".\"Id\" = @Id;";

            const string deleteIdentitySql = "DELETE FROM identity.\"Users\" WHERE identity.\"Users\".\"Id\" = @Id;";

            int affections = await connection.ExecuteAsync(
                deleteClientSql+deleteUserSql+deleteIdentitySql,
                new
                {
                    Id = command.ClientId
                });
            
            if (affections > 0)
            {
                await _eventBus.PublishAsync(new ClientDeletedIntegrationEvent(
                    Guid.NewGuid(),
                    command.ClientId));
            }
            else
            {
                throw new InvalidCommandException(["Client is not deleted"]);
            }
        }
    }
}