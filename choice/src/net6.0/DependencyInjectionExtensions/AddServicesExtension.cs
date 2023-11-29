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
using SelectelClient.Clients;
using SelectelClient.Models;

namespace Choice.DependencyInjectionExtensions
{
    static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            SelectelObjectClient.CredentialModel = new CredentialModel("ServiceUser", "129598Ec!", "283689", "My First Project");

            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IIndexStore, IndexStore>();
            services.AddScoped<ILoader, Loader>();

            services.AddSingleton<IHttpClientsFactory, HttpClientsFactory>();
            services.AddSingleton<IHttpClientService<Client>, HttpClientService<Client>>();
            services.AddSingleton<IHttpClientService<Company>, HttpClientService<Company>>();
            services.AddSingleton<IHttpClientService<Category>, HttpClientService<Category>>();
            services.AddSingleton<IHttpClientService<byte[]>, HttpClientService<byte[]>>();
            services.AddSingleton<IHttpClientService<Dictionary<string, string>>, HttpClientService<Dictionary<string, string>>>();
            services.AddScoped<IFileService, FileService>();

            services.AddSingleton<IApiService<Client>, ApiService<Client>>(s => CreateClientApiService(s));
            services.AddSingleton<IApiService<Company>, ApiService<Company>>(s => CreateCompanyApiService(s));
            services.AddSingleton<IApiService<Category>, ApiService<Category>>(s => CreateCategoryApiService(s));

            services.AddSingleton<IClientApiService, ClientApiService>();
            services.AddSingleton<ICompanyApiService, CompanyApiService>();
            services.AddSingleton<ICategoryApiService, CategoryApiService>();

            services.AddScoped<IAlertDialogService, AlertDialogService>();
            services.AddScoped<ICategoriesDialogService, CategoriesDialogService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

        private static ApiService<Client> CreateClientApiService(IServiceProvider services)
        {
            return new ApiService<Client>(services.GetRequiredService<IHttpClientService<Client>>(),
                                          "http://10.0.2.2/api");
        }

        private static ApiService<Category> CreateCategoryApiService(IServiceProvider services)
        {
            return new ApiService<Category>(services.GetRequiredService<IHttpClientService<Category>>(),
                                          "http://10.0.2.2/api");
        }

        private static ApiService<Company> CreateCompanyApiService(IServiceProvider services)
        {
            return new ApiService<Company>(services.GetRequiredService<IHttpClientService<Company>>(),
                                          "http://10.0.2.2/api");
        }
    }
}
