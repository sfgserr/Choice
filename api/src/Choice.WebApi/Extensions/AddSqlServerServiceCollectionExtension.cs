using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;
using Choice.Infrastructure;
using Choice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Choice.WebApi.Extensions
{
    public static class AddSqlServerServiceCollectionExtension
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChoiceContext>(o => o.UseSqlServer(configuration["Database:ConnectionString"]));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<ChatMessage>, ChatMessageRepository>();
            services.AddScoped<IRepository<Client>, ClientRepository>();
            services.AddScoped<IRepository<Company>, CompanyRepository>();
            services.AddScoped<IRepository<OrderMessage>, OrderMessageRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Review>, ReviewRepository>();

            return services;
        }
    }
}
