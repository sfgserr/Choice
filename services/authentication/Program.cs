using Choice.Authentication.Api.Data;
using Choice.Authentication.Api.Repositories;
using Choice.Authentication.Api.Services;
using Choice.Authentication;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Choice.EventBus.Messages.Common;
using Choice.Authentication.Api.Consumers;
using Microsoft.Identity.Client;
using Twilio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<UserContext>(o =>
    o.UseSqlServer(builder.Configuration["SqlServerSettings:ConnectionString"]));

string accountSid = builder.Configuration["TwilioSettings:AccountSid"];
string authToken = builder.Configuration["TwilioSettings:AuthToken"];

TwilioClient.Init(accountSid, authToken);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UserDataChangedConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.OrderStatusChangedQueue, c => {
            c.ConfigureConsumer<UserDataChangedConsumer>(ctx);
        });
    });
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