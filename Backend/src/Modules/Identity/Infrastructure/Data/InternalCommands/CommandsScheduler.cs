using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace Identity.Infrastructure.Data.InternalCommands
{
    internal class CommandsScheduler : ICommandsScheduler
    {
        private readonly IdentityContext _identityContext;

        internal CommandsScheduler(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task EnqueueAsync(InternalCommandBase command)
        {
            var type = command.GetType().FullName!;

            var json = JsonConvert.SerializeObject(command, new JsonSerializerSettings()
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var internalCommand = new InternalCommand(command.Id, type, json);

            await _identityContext.InternalCommands.AddAsync(internalCommand);
        }
    }
}