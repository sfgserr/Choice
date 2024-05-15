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
using Choice.Chat.Api.Infrastructure.Data;
using Choice.Chat.Api.Services;

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
            builder.Services.AddSingleton<ChatService>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<ChatDdContext>(o => 
                o.UseNpgsql(builder.Configuration["PostgreSqlSettings:ConnectionString"]));
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderEnrollmentDateChangedConsumer>();
                config.AddConsumer<OrderCreatedConsumer>();
                config.AddConsumer<OrderStatusChangedConsumer>();
                config.AddConsumer<UserEnrolledConsumer>();
                config.AddConsumer<UserCreatedConsumer>();
                config.AddConsumer<UserIconUriChangedConsumer>();
                config.AddConsumer<UserDataChangedConsumer>();
                config.AddConsumer<OrderEnrollmentDateConfirmedConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.OrderEnrollmentDateChangedQueue, c => {
                        c.ConfigureConsumer<OrderEnrollmentDateChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.OrderCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<OrderCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.OrderMessageStatusChangedQueue, c =>
                    {
                        c.ConfigureConsumer<OrderStatusChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UserEnrolledQueue, c =>
                    {
                        c.ConfigureConsumer<UserEnrolledConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UserCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<UserCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UserIconUriChangedQueue, c =>
                    {
                        c.ConfigureConsumer<UserIconUriChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.ChatUserNameChangedQueue, c =>
                    {
                        c.ConfigureConsumer<UserDataChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.OrderEnrollmentDateConfirmedQueue, c =>
                    {
                        c.ConfigureConsumer<OrderEnrollmentDateConfirmedConsumer>(ctx);
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