using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payments.Infrastructure.Configuration;
using Payments.Infrastructure.Data;
using Polly;

namespace Payments.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly PaymentsContext _paymentsContext;

        internal ProcessInternalCommandsCommandHandler(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
        }

        public async Task Execute(ProcessInternalCommandsCommand command)
        {
            var internalCommands = await _paymentsContext.InternalCommands
                .Where(c => c.Processed == null)
                .ToListAsync();

            var policy = Policy.Handle<Exception>()
                    .WaitAndRetryAsync(
                    [
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(3),
                    ]);

            foreach (var internalCommand in internalCommands)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    internalCommand.Processed = DateTime.UtcNow;
                    internalCommand.Error = result.FinalException.Message;
                }
            }
        }

        private async Task ProcessCommand(InternalCommand command)
        {
            Type commandType = Assemblies.Application.GetType(command.Type)!;

            dynamic internalCommandBase = JsonConvert.DeserializeObject(command.Data, commandType)!;

            await CommandsExecutor.ExecuteCommandAsync(internalCommandBase);
        }
    }
}
