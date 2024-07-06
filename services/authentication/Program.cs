using Choice.Authentication.Api.Data;
using Choice.Authentication.Api.Services;
using Choice.Authentication;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Choice.EventBus.Messages.Common;
using Choice.Authentication.Api.Consumers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Choice.Authentication.Api.Models;
using Authentication.Api.Consumers;
using Vonage.Request;
using Authentication.Api.Services;
using Vonage.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserContext>(o =>
    o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));
builder.Services.AddIdentityCore<User>(options =>
{
    options.User.AllowedUserNameCharacters = null;
})
.AddEntityFrameworkStores<UserContext>();
builder.Services.AddScoped<ITokenService, TokenService>();

string apiKey = builder.Configuration["VonageSettings:ApiKey"]!;
string apiSecret = builder.Configuration["VonageSettings:ApiSecret"]!;

var credentials = Credentials.FromApiKeyAndSecret(apiKey, apiSecret);

builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddVonageClientScoped(credentials);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UserDataChangedConsumer>();
    config.AddConsumer<CompanyDataFilledConsumer>();
    config.AddConsumer<UserDeletedConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.UserDataChangedQueue, c => 
        {
            c.ConfigureConsumer<UserDataChangedConsumer>(ctx);
        });

        cfg.ReceiveEndpoint(EventBusConstants.CompanyDataFilledQueue, c =>
        {
            c.ConfigureConsumer<CompanyDataFilledConsumer>(ctx);
        });

        cfg.ReceiveEndpoint(EventBusConstants.UserDeletedQueue, c =>
        {
            c.ConfigureConsumer<UserDeletedConsumer>(ctx);
        });
    });
});

string issuerKey = builder.Configuration["JwtSettings:Key"];

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

var app = builder.Build();

await UsersSeed.Seed(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();