using BuildingBlocks.Application.Cqrs.Commands;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Infrastructure.Processing
{
    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<T> _decorated;

        internal LoggingCommandHandlerDecorator(ILogger logger, ICommandHandler<T> decorated)
        {
            _logger = logger;
            _decorated = decorated;
        }

        public async Task Execute(T command)
        {
            if (command is IRecurringCommand)
            {
                await _decorated.Execute(command);
                return;
            }

            string commandName = command.GetType().Name;

            using (LogContext.Push(new CommandLogEnricher(command)))
            {
                try
                {
                    _logger.Information("Command:{Command} executing", commandName);

                    await _decorated.Execute(command);

                    _logger.Information($"Executed successfully");
                }
                catch (Exception ex)
                {
                    _logger.Error($"Command:{commandName} failed with error {ex.Message}");
                    throw;
                }
            }
        }

        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommand _command;

            public CommandLogEnricher(ICommand command)
            {
                _command = command;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty(
                    "Context", 
                    new ScalarValue($"Command:{_command.GetType().Name}")));
            }
        }
    }
}
