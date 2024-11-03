using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace Chat.Infrastructure.Data.InternalCommands
{
    internal class CommandsScheduler
    {
        private readonly ChatContext _chatContext;

        internal CommandsScheduler(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task EnqueueAsync(InternalCommandBase command)
        {
            string type = command.GetType().FullName!;

            string json = JsonConvert.SerializeObject(command, new JsonSerializerSettings()
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var internalCommand = new InternalCommand(command.Id, type, json);

            await _chatContext.InternalCommands.AddAsync(internalCommand);
        }
    }
}