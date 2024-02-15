using Choice.Authentication.EventBusConsumer;
using Choice.Authentication.Infrastructure.Data;
using Choice.Authentication.Infrastructure.Data.Repositories;
using Choice.Authentication.Infrastructure.Verification;
using Choice.Authentication.Infrastructure.Verification.Interfaces;
using Choice.Authentication.Services;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Twilio;

namespace Choice.Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UserContext>(o =>
                o.UseNpgsql(builder.Configuration["PostgreSql:ConnectionString"]));
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<ISmsService, SmsService>(s =>
                new SmsService(builder.Configuration["Twilio:ServiceSID"]));
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<UserCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration["EventBus:Host"]);
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            TwilioClient.Init(builder.Configuration["Twilio:AccountSID"], builder.Configuration["Twilio:Token"]);

            app.Run();
        }
    }
}