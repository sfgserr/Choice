using Autofac;
using Autofac.Features.Variance;
using BuildingBlocks.Infrastructure.Configuration;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Chat.Infrastructure.Configuration.Mediation
{
    internal class MediationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterType<ServiceProviderWrapper>()
                .As<IServiceProvider>()
                .InstancePerDependency()
                .IfNotRegistered(typeof(IServiceProvider))
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var types = new Type[]
            {
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            };

            foreach (var type in types)
            {
                builder.RegisterAssemblyTypes(Assemblies.Application, ThisAssembly)
                    .AsClosedTypesOf(type)
                    .AsImplementedInterfaces()
                    .InstancePerDependency()
                    .FindConstructorsWith(new AllConstructorFinder());
            }
        }
    }
}