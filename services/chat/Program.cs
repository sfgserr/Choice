using Chat.Extensions;
using Choice.Chat.Consumers;
using Choice.Chat.Hubs;
using Choice.Chat.Repositories;
using Choice.EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Choice.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSignalR();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderChangedConsumer>();
                config.AddConsumer<OrderCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.OrderChangedQueue, c => {
                        c.ConfigureConsumer<OrderChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.OrderCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<OrderCreatedConsumer>(ctx);
                    });
                });
            });

            var app = builder.Build();
            app.MigrateDatabase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapHub<ChatHub>("/chat");
            app.MapControllers();

            app.Run();
        }
    }
}