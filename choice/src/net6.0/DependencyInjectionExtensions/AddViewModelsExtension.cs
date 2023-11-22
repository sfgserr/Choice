
using Choice.ViewModels;

namespace Choice.DependencyInjectionExtensions
{
    static class AddViewModelsExtension
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<RegisterClientViewModel>();
            services.AddScoped<RegisterCompanyViewModel>();
            services.AddScoped<LoginByEmailViewModel>();
            services.AddScoped<LoginByPhoneViewModel>();
            services.AddScoped<CompanyCardViewModel>();
            services.AddScoped<CategoryViewModel>();
            services.AddScoped<CategoryMapViewModel>();

            return services;
        }
    }
}
