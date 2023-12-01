
using Choice.Pages;

namespace Choice.DependencyInjectionExtensions
{
    static class AddPagesExtension
    {
        public static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddScoped<LoginPage>();
            services.AddScoped<AccountPage>();
            services.AddScoped<CategoryMapPage>();
            services.AddScoped<CategoryPage>();
            services.AddScoped<ChatPage>();
            services.AddScoped<CompanyCardPage>();
            services.AddScoped<OrderPage>();
            services.AddScoped<RegisterClientPage>();
            services.AddScoped<RegisterCompanyPage>();

            return services;
        }
    }
}
