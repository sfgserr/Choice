﻿using Choice.Domain.Models;
using Choice.Pages;
using Choice.Services.ApiServices;
using Choice.Services.AuthenticationServices;
using Choice.Services.HttpClientServices;
using Choice.Stores.Authenticators;
using Choice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

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

                    services.AddSingleton<HttpClient>(s => new HttpClient());

                    services.AddScoped<IHttpClientService<Client>, HttpClientService<Client>>();
                    services.AddScoped<IHttpClientService<Company>, HttpClientService<Company>>();

                    services.AddScoped<IApiService<Client>, ApiService<Client>>(s => CreateClientApiService(s));
                    services.AddScoped<IApiService<Company>, ApiService<Company>>(s => CreateCompanyApiService(s));

                    services.AddScoped<IAuthentictionService, AuthenticationService>();
                });

        private static ApiService<Client> CreateClientApiService(IServiceProvider services)
        {
            return new ApiService<Client>(services.GetRequiredService<IHttpClientService<Client>>(),
                                          "https://choice.webapi.azurewebsites.net/api");
        }

        private static ApiService<Company> CreateCompanyApiService(IServiceProvider services)
        {
            return new ApiService<Company>(services.GetRequiredService<IHttpClientService<Company>>(),
                                          "https://choice.webapi.azurewebsites.net/api");
        }
    }
}
