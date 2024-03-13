using Choice.Application.Services;
using Choice.Infrastructure.Authentication;
using Choice.Infrastructure.Data;
using Choice.Ordering.Domain.OrderEntity;
using Choice.Ordering.Grpc.Services;
using Choice.Ordering.Infrastructure.Data;
using Choice.Ordering.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(s => new(s.GetRequiredService<OrderingContext>()));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<OrderingContext>(o =>
                o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<OrderingService>();

            app.Run();
        }
    }
}