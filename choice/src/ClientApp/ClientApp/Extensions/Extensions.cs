using ClientApp.Services.Mappers;
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
            services.AddScoped<LoginByPhoneViewModel>();
            services.AddScoped<LoginByEmailViewModel>();

            services.AddScoped<LoginView>();
            services.AddScoped<LoginByPhoneView>();
            services.AddScoped<LoginByEmailView>();

            services.AddSingleton<INavigator, Navigator>();

            services.AddScoped<ViewModelMapper>();

            services.AddScoped<ViewMapperDataTemplate>();

            return services;
        }
    }
}
