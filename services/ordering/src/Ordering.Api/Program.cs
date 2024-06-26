using Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate;
using Choice.Ordering.Application.UseCases.CreateOrder;
using Choice.Ordering.Application.UseCases.Enroll;
using Choice.Ordering.Application.UseCases.FinishOrder;
using Choice.Ordering.Application.UseCases.CancelEnrollment;
using Choice.Ordering.Application.UseCases.GetOrders;
using Choice.Ordering.Application.UseCases.ConfirmEnrollmentDate;
using Choice.Ordering.Domain.OrderEntity;
using Choice.Ordering.Infrastructure.Data;
using Choice.Ordering.Infrastructure.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Choice.Application.Services;
using Choice.Infrastructure.Data;
using Choice.Infrastructure.Authentication;

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
            builder.Services.AddScoped<IGetOrdersUseCase, GetOrdersUseCase>();
            builder.Services.Decorate<ICreateOrderUseCase, CreateOrderValidationUseCase>();
            builder.Services.AddScoped<IEnrollUseCase, EnrollUseCase>();
            builder.Services.AddScoped<IFinishOrderUseCase, FinishOrderUseCase>();
            builder.Services.AddScoped<IConfirmEnrollmentDateUseCase, ConfirmEnrollmentDateUseCase>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(s => new(s.GetRequiredService<OrderingContext>()));
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

            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("Company", policy =>
                {
                    policy.RequireClaim("type", "Company");
                    policy.RequireClaim("isDataFilled", "true");
                });
                o.AddPolicy("Client", policy => policy.RequireClaim("type", "Client"));

                o.AddPolicy("Default", policy =>
                {
                    policy.RequireClaim("type", "Company", "Client");
                    policy.RequireClaim("isDataFilled", "true");
                });

                o.DefaultPolicy = o.GetPolicy("Default")!;
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