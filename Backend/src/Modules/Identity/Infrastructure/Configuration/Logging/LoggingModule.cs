using Autofac;
using ILogger = Serilog.ILogger;

namespace Identity.Infrastructure.Configuration.Logging
{
    internal class LoggingModule : Module
    {
        private readonly ILogger _logger;

        internal LoggingModule(ILogger logger)
        {
            _logger = logger;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_logger)
                .As<ILogger>()
                .SingleInstance();
        }
    }
}