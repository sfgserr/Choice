using Choice.EventBus.Messages.Common;
using Choice.ReviewService.Api.Consumers;
using Choice.ReviewService.Api.Infrastructure.Data;
using Choice.ReviewService.Api.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Choice.ReviewService.Api.Services;
using System.Text;
using Choice.Ordering.Grpc.Protos;
using ReviewService.Api.Consumers;

namespace Choice.ReviewService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddGrpcClient<OrderingProtoService.OrderingProtoServiceClient>(o =>
            {
                o.Address = new Uri(builder.Configuration["GrpcSettings:Url"]);
            });
            builder.Services.AddScoped<OrderingService>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddDbContext<ReviewContext>(o => 
                o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<AuthorCreatedConsumer>();
                config.AddConsumer<UserDataChangedConsumer>();
                config.AddConsumer<UserIconUriChangedConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.AuthorCreatedQueue, c => {
                        c.ConfigureConsumer<AuthorCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UserDataChangedQueue, c => { 
                        c.ConfigureConsumer<UserDataChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.UserIconUriChangedQueue, c => {
                        c.ConfigureConsumer<UserIconUriChangedConsumer>(ctx);
                    });
                });
            });

            builder.Services.AddHttpClient("Ordering", o => 
                o.BaseAddress = new(builder.Configuration["OrderingSettings:Url"]));

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