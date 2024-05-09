using Choice.Chat.Api.Extensions;
using Choice.Chat.Api.Consumers;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Repositories;
using Choice.EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Entities;
using Choice.EventBus.Messages.Events;

namespace Choice.Chat.Api
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
            builder.Services.AddScoped<IRepository<Message>, MessageRepository>();
            builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderEnrollmentDateChangedConsumer>();
                config.AddConsumer<OrderCreatedConsumer>();
                config.AddConsumer<OrderStatusChangedConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.OrderEnrollmentDateChangedQueue, c => {
                        c.ConfigureConsumer<OrderEnrollmentDateChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.OrderCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<OrderCreatedConsumer>(ctx);
                    });
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("_allowsAny",
                    builder =>
                    {
                        // Not a permanent solution, but just trying to isolate the problem
                        builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });

            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            string issuerKey = builder.Configuration["JwtSettings:Key"]!;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerKey))
                    };
                });

            builder.Services.AddAuthorization(auth =>
            {
                auth.AddPolicy("AuthUser", policy =>
                {
                    policy.RequireClaim("type", "Client", "Company");
                    policy.RequireClaim("isDataFilled", "true");
                });
                auth.DefaultPolicy = auth.GetPolicy("AuthUser")!;
            });

            var app = builder.Build();
            app.MigrateDatabase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseForwardedHeaders();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.MapHub<ChatHub>("/chat");

            app.Run();
        }
    }
}