using Choice.Authentication.EventBusConsumer;
using Choice.Authentication.Infrastructure.Data;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Choice.Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UserContext>(o =>
                o.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddRazorPages();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<UserCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@host.docker.internal:5672");
                    cfg.ReceiveEndpoint(EventBusConstants.UserCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<UserCreatedConsumer>(ctx);
                    });
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}