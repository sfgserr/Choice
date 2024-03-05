using Choice.ClientService.Application.Services;
using Choice.ClientService.Application.UseCases.ChangeUserData;
using Choice.ClientService.Application.UseCases.GetClientRequests;
using Choice.ClientService.Application.UseCases.GetClients;
using Choice.ClientService.Application.UseCases.SendOrderRequest;
using Choice.ClientService.Infrastructure.Authentication;
using Choice.ClientService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.Decorate<IChangeUserDataUseCase, ChangeUserDataValidationUseCase>();
            builder.Services.Decorate<ISendOrderRequestUseCase, SendOrderRequestValidationUseCase>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<Notification>();

            builder.Services.AddDbContext<ClientContext>(o =>
                o.UseSqlServer(builder.Configuration["SqlServerSettings:"]));
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

            app.UseHttpsRedirection();
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