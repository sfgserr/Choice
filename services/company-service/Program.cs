using Choice.Application.Services;
using Choice.CompanyService.Api.Consumers;
using Choice.CompanyService.Api.Infrastructure.Data;
using Choice.CompanyService.Api.Repositories;
using Choice.EventBus.Messages.Common;
using Choice.Infrastructure.Geolocation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Choice.CompanyService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<UserCreatedConsumer>();
                config.AddConsumer<ReviewLeftConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.CompanyCreatedQueue, c => {
                        c.ConfigureConsumer<UserCreatedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.CompanyAverageGradeChangedQueue, c =>
                    {
                        c.ConfigureConsumer<ReviewLeftConsumer>(ctx);
                    });
                });
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

            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("Client", policy => policy.RequireClaim(ClaimTypes.Email));
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddAuthorization(o => o.AddPolicy("Company", policy => policy.RequireClaim("address")));
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddDbContext<CompanyContext>(o =>
                o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));
            builder.Services.AddScoped<IAddressService, AddressService>(s =>
                new(s.GetRequiredService<HttpClient>(), new(builder.Configuration["GeoapifySettings:ApiKey"])));
            builder.Services.AddEndpointsApiExplorer();
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