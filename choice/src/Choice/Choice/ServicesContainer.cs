using Choice.Dialogs.AccountCreatedDialogs;
using Choice.Dialogs.CategoriesDialogs;
using Choice.Domain.Models;
using Choice.Factories;
using Choice.Services.ApiServices;
using Choice.Services.AuthenticationServices;
using Choice.Services.CategoryApiServices;
using Choice.Services.ClientApiServices;
using Choice.Services.CompanyApiService;
using Choice.Services.FileServices;
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

                    Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "AAPK30dd6b1d7168422da964d85fe40b8d47eiFScqDEGux44C-46YPRf8Mj_2hm_gOKDPlDmWgHvGqqXRNKeZF8IAEJDeyq303t";

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
                    services.AddScoped<CategoryViewModel>();

                    services.AddSingleton<IHttpClientsFactory, HttpClientsFactory>();
                    services.AddSingleton<IHttpClientService<Client>, HttpClientService<Client>>();
                    services.AddSingleton<IHttpClientService<Company>, HttpClientService<Company>>();
                    services.AddSingleton<IHttpClientService<Category>, HttpClientService<Category>>();
                    services.AddSingleton<IHttpClientService<byte[]>, HttpClientService<byte[]>>();
                    services.AddSingleton<IFileService, FileService>();

                    services.AddSingleton<IApiService<Client>, ApiService<Client>>(s => CreateClientApiService(s));
                    services.AddSingleton<IApiService<Company>, ApiService<Company>>(s => CreateCompanyApiService(s));
                    services.AddSingleton<IApiService<Category>, ApiService<Category>>(s => CreateCategoryApiService(s));

                    services.AddSingleton<IClientApiService, ClientApiService>();
                    services.AddSingleton<ICompanyApiService, CompanyApiService>();
                    services.AddSingleton<ICategoryApiService, CategoryApiService>();

                    services.AddScoped<IAlertDialogService, AlertDialogService>();
                    services.AddScoped<ICategoriesDialogService, CategoriesDialogService>();

                    services.AddScoped<IAuthenticationService, AuthenticationService>();
                });

        private static ApiService<Client> CreateClientApiService(IServiceProvider services)
        {
            return new ApiService<Client>(services.GetRequiredService<IHttpClientService<Client>>(),
                                          "http://5.35.13.28/api");
        }

        private static ApiService<Category> CreateCategoryApiService(IServiceProvider services)
        {
            return new ApiService<Category>(services.GetRequiredService<IHttpClientService<Category>>(),
                                          "http://5.35.13.28/api");
        }

        private static ApiService<Company> CreateCompanyApiService(IServiceProvider services)
        {
            return new ApiService<Company>(services.GetRequiredService<IHttpClientService<Company>>(),
                                          "http://5.35.13.28/api");
        }
    }
}
