using ClientApp.Stores.Navigator;
using ClientApp.ViewModels;
using ClientApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ClientApp.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<LoginViewModel>();

            services.AddScoped<LoginView>();

            services.AddSingleton<INavigator, Navigator>();

            services.AddScoped<ViewMapper>();

            return services;
        }
    }
}
