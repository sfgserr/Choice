using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Text.Json;
using Users.Infrastructure.Configuration;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly UsersContext _usersContext;

        internal ProcessInternalCommandsCommandHandler(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task Execute(ProcessInternalCommandsCommand command)
        {
            var internalCommands = await _usersContext.InternalCommands
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
