using Autofac;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Infrastructure.Configuration;
using BuildingBlocks.Infrastructure.Processing;
using Chat.Infrastructure.Data.InternalCommands;
using Chat.Infrastructure.Processing;

namespace Chat.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandsScheduler>()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterAssemblyTypes(Assemblies.Application, ThisAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
            
            builder.RegisterAssemblyTypes(Assemblies.Application, ThisAssembly)
                .AsClosedTypesOf(typeof(ICommandHandlerWithResult<,>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
            
            builder.RegisterAssemblyTypes(Assemblies.Application, ThisAssembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));
            
            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandlerWithResult<,>));
            
            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));
            
            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandlerWithResult<,>));
            
            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));
            
            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandlerWithResult<,>));
        }
    }
}
