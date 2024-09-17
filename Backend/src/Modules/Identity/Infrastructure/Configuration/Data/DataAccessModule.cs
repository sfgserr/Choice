using Autofac;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.Data;
using BuildingBlocks.Infrastructure.Data.ValueConversion;
using Identity.Application.Contracts;
using Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identity.Infrastructure.Configuration.Data
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
                var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

                optionsBuilder.UseNpgsql(_connectionString);
                optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new IdentityContext(optionsBuilder.Options);
            })
            .AsSelf()
            .As<DbContext>()
            .As<IIdentityDbContext>()
            .InstancePerLifetimeScope();

            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();
            
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}