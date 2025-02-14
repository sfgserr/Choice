using Autofac;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.Data;
using BuildingBlocks.Infrastructure.Data.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.Application.Contracts;
using Users.Application.OrderRequests;
using Users.Application.Users;
using Users.Domain.OrderRequests;
using Users.Domain.Users;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.Configuration.Data
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

                return new UsersContext(optionsBuilder.Options);
            })
            .As<DbContext>()
            .As<IUsersDbContext>()
            .AsSelf()
            .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<OrderResponsesCounter>()
                .As<IOrderResponsesCounter>()
                .InstancePerLifetimeScope();
        }
    }
}
