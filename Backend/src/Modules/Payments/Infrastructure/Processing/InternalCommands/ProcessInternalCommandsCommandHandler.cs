using BuildingBlocks.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Payments.Infrastructure.Configuration;
using Payments.Infrastructure.Data;
using Polly;
using System.Text.Json;

namespace Payments.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler
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
                var internalCommandBase = JsonSerializer.Deserialize(
                    internalCommand.Data,
                    Type.GetType(internalCommand.Type)!);

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

            dynamic internalCommandBase = JsonSerializer.Deserialize(command.Data, commandType)!;

            await CommandsExecutor.ExecuteCommandAsync(internalCommandBase);
        }
    }
}
