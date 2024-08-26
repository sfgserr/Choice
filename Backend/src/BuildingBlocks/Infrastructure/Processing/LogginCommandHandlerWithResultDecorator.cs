using BuildingBlocks.Application.Cqrs.Commands;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Infrastructure.Processing
{
    internal class LogginCommandHandlerWithResultDecorator<T, TResult> :
        ICommandHandlerWithResult<T, TResult> where T : ICommandWithResult<TResult>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandlerWithResult<T, TResult> _decorated;

        internal LogginCommandHandlerWithResultDecorator(
            ILogger logger, 
            ICommandHandlerWithResult<T, TResult> decorated)
        {
            _logger = logger;
            _decorated = decorated;
        }

        public async Task<TResult> Execute(T command)
        {
            if (command is IRecurringCommand)
            {
                return await _decorated.Execute(command);   
            }

            string commandName = command.GetType().Name;    

            using (LogContext.Push(new CommandLogEnricher(command)))
            {
                try
                {
                    _logger.Information("Command with result:{Command} executing", commandName);

                    var result = await _decorated.Execute(command);

                    _logger.Information(
                        "Command with result:{Command} executed with result:{Result}", 
                        commandName, 
                        result);

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.Error(
                        "Command with result:{Command} failed with error:{Error}",
                        commandName,
                        ex.Message);

                    throw;
                }
            }
        }

        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommandWithResult<TResult> _command;

            public CommandLogEnricher(ICommandWithResult<TResult> command)
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
