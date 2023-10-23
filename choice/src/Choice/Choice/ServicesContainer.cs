using Choice.Pages;
using Choice.Stores.Authenticators;
using Choice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Choice
{
    //AntiPattern
    public static class ServicesContainer
    {
        private static IHost _host;

        public static void SetUpContainer() =>
            _host = CreateDefaultBuilder().Build();

        public static T GetService<T>() where T : class =>
            _host.Services.GetRequiredService<T>();

        private static IHostBuilder CreateDefaultBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IAuthenticator, Authenticator>();

                    services.AddScoped<MainViewModel>();
                    services.AddScoped<LoginViewModel>();
                });
    }
}
