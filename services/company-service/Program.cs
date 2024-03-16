using Choice.Application.Services;
using Choice.CompanyService.Api.Consumers;
using Choice.CompanyService.Api.Infrastructure.Data;
using Choice.CompanyService.Api.Repositories;
using Choice.EventBus.Messages.Common;
using Choice.Infrastructure.Geolocation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddHttpClient();
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