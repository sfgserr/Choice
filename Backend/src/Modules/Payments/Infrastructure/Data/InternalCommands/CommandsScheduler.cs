using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace Payments.Infrastructure.Data.InternalCommands
{
    internal class CommandsScheduler
    {
        private readonly PaymentsContext _paymentsContext;

        internal CommandsScheduler(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
        }

        public async Task EnqueueAsync(InternalCommandBase command)
        {
            string type = command.GetType().FullName!;

            string json = JsonConvert.SerializeObject(command, new JsonSerializerSettings()
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var internalCommand = new InternalCommand(command.Id, type, json);

            await _paymentsContext.InternalCommands.AddAsync(internalCommand);
        }
    }
}
