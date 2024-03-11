using Choice.EventBus.Messages.Common;
using Choice.ReviewService.Api.Consumers;
using Choice.ReviewService.Api.Infrastructure.Data;
using Choice.ReviewService.Api.Infrastructure.Ordering;
using Choice.ReviewService.Api.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Choice.ReviewService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IOrderingService, OrderingService>();
            builder.Services.AddDbContext<ReviewContext>(o => 
                o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<UserCreatedConsumer>();
                config.AddConsumer<UserDataChangedConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.UserCreatedQueue, c => {
                        c.ConfigureConsumer<UserCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UserDataChangedQueue, c =>
                    {
                        c.ConfigureConsumer<UserDataChangedConsumer>(ctx);
                    });
                });
            });

            builder.Services.AddHttpClient("Client", o => o.BaseAddress = new("http://localhost"));

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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}