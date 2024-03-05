using Choice.Ordering.Application.Services;
using Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate;
using Choice.Ordering.Application.UseCases.CreateOrder;
using Choice.Ordering.Application.UseCases.Enroll;
using Choice.Ordering.Application.UseCases.FinishOrder;
using Choice.Ordering.Domain.OrderEntity;
using Choice.Ordering.Infrastructure.Authentication;
using Choice.Ordering.Infrastructure.Data;
using Choice.Ordering.Infrastructure.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.UseCases.CancelEnrollment;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Choice.Ordering.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<ICancelEnrollmentUseCase, CancelEnrollmentUseCase>();
            builder.Services.AddScoped<IChangeOrderEnrollmentDateUseCase, ChangeOrderEnrollmentDateUseCase>();
            builder.Services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
            builder.Services.Decorate<ICreateOrderUseCase, CreateOrderValidationUseCase>();
            builder.Services.AddScoped<IEnrollUseCase, EnrollUseCase>();
            builder.Services.AddScoped<IFinishOrderUseCase, FinishOrderUseCase>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<Notification>();
            builder.Services.AddDbContext<OrderingContext>(o =>
                o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));

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

            builder.Services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
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

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseForwardedHeaders();
            app.UseCors("_allowsAny");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}