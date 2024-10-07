using Autofac;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.Data.ValueConversion;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Payments.Infrastructure.Data;
using Payments.Application.Contracts;
using Payments.Domain.SubscritpionPayments;
using Payments.Application.Subscriptions;

namespace Payments.Infrastructure.Configuration.Data
{
    internal class DataAccessModule : Module
    {
        private readonly string _connectionString;

        internal DataAccessModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseNpgsql(_connectionString);

                optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new PaymentsContext(optionsBuilder.Options);
            })
            .As<DbContext>()
            .As<IPaymentsDbContext>()
            .AsSelf()
            .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<SubscriptionsCounter>()
                .As<ISubscriptionsCounter>()
                .InstancePerLifetimeScope();
        }
    }
}
