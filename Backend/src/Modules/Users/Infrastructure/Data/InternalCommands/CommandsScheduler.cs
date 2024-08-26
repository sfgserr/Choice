using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace Users.Infrastructure.Data.InternalCommands
{
    internal class CommandsScheduler : ICommandsScheduler
    {
        private readonly UsersContext _usersContext;

        internal CommandsScheduler(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task EnqueueAsync(InternalCommandBase command)
        {
            string type = command.GetType().FullName!;

            string json = JsonConvert.SerializeObject(command, new JsonSerializerSettings()
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var internalCommand = new InternalCommand(command.Id, type, json);

            await _usersContext.InternalCommands.AddAsync(internalCommand);
        }
    }
}
