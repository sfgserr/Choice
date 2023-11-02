using Choice.Dialogs;
using Choice.Domain.Models;
using Choice.Pages;
using Choice.Services.ApiServices;
using Choice.Services.AuthenticationServices;
using Choice.Services.HttpClientServices;
using Choice.Stores.Authenticators;
using Choice.Stores.IndexStores;
using Choice.Stores.Loaders;
using Choice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Twilio;

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
                    TwilioClient.Init("ACdf6bfdcacd0f2f8c967a755e67a685a8", "37b8460e2ce3ea57b4bb5e75e0798688");
                    

                    services.AddSingleton<IAuthenticator, Authenticator>();
                    services.AddSingleton<IIndexStore, IndexStore>();
                    services.AddScoped<ILoader, Loader>();

                    services.AddScoped<MainViewModel>();
                    services.AddScoped<LoginViewModel>();
                    services.AddScoped<RegisterClientViewModel>();
                    services.AddScoped<RegisterCompanyViewModel>();
                    services.AddScoped<LoginByEmailViewModel>();
                    services.AddScoped<LoginByPhoneViewModel>();
                    services.AddScoped<CompanyCardViewModel>();

                    services.AddSingleton<HttpClient>(s => new HttpClient());

                    services.AddScoped<IHttpClientService<Client>, HttpClientService<Client>>();
                    services.AddScoped<IHttpClientService<Company>, HttpClientService<Company>>();

                    services.AddScoped<IApiService<Client>, ApiService<Client>>(s => CreateClientApiService(s));
                    services.AddScoped<IApiService<Company>, ApiService<Company>>(s => CreateCompanyApiService(s));

                    services.AddScoped<IAlertDialogService, AlertDialogService>();

                    services.AddScoped<IAuthenticationService, AuthenticationService>();
                });

        private static ApiService<Client> CreateClientApiService(IServiceProvider services)
        {
            return new ApiService<Client>(services.GetRequiredService<IHttpClientService<Client>>(),
                                          "https://choiceweb.azurewebsites.net/api");
        }

        private static ApiService<Company> CreateCompanyApiService(IServiceProvider services)
        {
            return new ApiService<Company>(services.GetRequiredService<IHttpClientService<Company>>(),
                                          "https://choiceweb.azurewebsites.net/api");
        }
    }
}
