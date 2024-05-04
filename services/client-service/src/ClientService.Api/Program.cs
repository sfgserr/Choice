using Choice.Application.Services;
using Choice.ClientService.Api.Consumers;
using Choice.ClientService.Application.Services;
using Choice.ClientService.Application.UseCases.ChangeUserData;
using Choice.ClientService.Application.UseCases.GetClient;
using Choice.ClientService.Application.UseCases.GetClientRequests;
using Choice.ClientService.Application.UseCases.GetClients;
using Choice.ClientService.Application.UseCases.GetOrderRequests;
using Choice.ClientService.Application.UseCases.SendOrderRequest;
using Choice.ClientService.Application.UseCases.ChangeOrderRequest;
using Choice.ClientService.Application.UseCases.ChangeIconUri;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Infrastructure.Authentication;
using Choice.ClientService.Infrastructure.Data;
using Choice.ClientService.Infrastructure.Data.Repositories;
using Choice.Infrastructure.Geolocation;
using Choice.EventBus.Messages.Common;
using Choice.Infrastructure.Authentication;
using Choice.Infrastructure.Data;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Choice.ClientService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IChangeUserDataUseCase, ChangeUserDataUseCase>();
            builder.Services.AddScoped<IGetClientsUseCase, GetClientsUseCase>();
            builder.Services.AddScoped<ISendOrderRequestUseCase, SendOrderRequestUseCase>();
            builder.Services.AddScoped<IGetClientRequestsUseCase, GetClientRequestsUseCase>();
            builder.Services.AddScoped<IGetOrderRequestsUseCase, GetOrderRequestsUseCase>();
            builder.Services.AddScoped<IGetClientUseCase, GetClientUseCase>();
            builder.Services.AddScoped<IChangeOrderRequestUseCase, ChangeOrderRequestUseCase>();
            builder.Services.AddScoped<IChangeIconUriUseCase, ChangeIconUriUseCase>();
            builder.Services.Decorate<IChangeUserDataUseCase, ChangeUserDataValidationUseCase>();
            builder.Services.Decorate<ISendOrderRequestUseCase, SendOrderRequestValidationUseCase>();
            builder.Services.Decorate<IChangeOrderRequestUseCase, ChangeOrderRequestValidationUseCase>();
            builder.Services.Decorate<IChangeIconUriUseCase, ChangeIconUriValidationUseCase>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<Notification>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(s => new(s.GetRequiredService<ClientContext>()));
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderStatusChangedConsumer>();
                config.AddConsumer<ReviewLeftConsumer>();
                config.AddConsumer<UserCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.OrderStatusChangedQueue, c => {
                        c.ConfigureConsumer<OrderStatusChangedConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.ClientAverageGradeChangedQueue, c =>
                    {
                        c.ConfigureConsumer<ReviewLeftConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.ClientCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<UserCreatedConsumer>(ctx);
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
                o.AddPolicy("Company", policy =>
                {
                    policy.RequireClaim("type", "Company");
                    policy.RequireClaim("isDataFilled", "true");
                }));

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IAddressService, AddressService>(s =>
                new(s.GetRequiredService<HttpClient>(), new(builder.Configuration["GeoapifySettings:ApiKey"])));
            builder.Services.AddDbContext<ClientContext>(o =>
                o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
            app.Run();
        }
    }
}