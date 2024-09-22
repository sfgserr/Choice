using System.Text.Json;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using Identity.Infrastructure.Configuration;
using Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Identity.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly IdentityContext _identityContext;

        internal ProcessInternalCommandsCommandHandler(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task Execute(ProcessInternalCommandsCommand command)
        {
            var internalCommands = await _identityContext.InternalCommands
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
                    internalCommand.Processed = DateTime.Now;
                    internalCommand.Error = result.FinalException.Message;
                }
            }
        }

        private async Task ProcessCommand(InternalCommand command)
        {
            var commandType = Assemblies.Application.GetType(command.Type)!;

            dynamic internalCommandBase = JsonSerializer.Deserialize(command.Data, commandType)!;
            
            await CommandsExecutor.ExecuteCommandAsync(internalCommandBase);
        }
    }
}