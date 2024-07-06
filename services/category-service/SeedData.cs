using Choice.CategoryService.Api.Entities;
using Choice.CategoryService.Api.Repositories;

namespace Choice.CategoryService.Api
{
    public static class SeedData
    {
        public static async Task Seed(IServiceProvider services)
        {
            var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            ICategoryRepository repository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();

            List<Category> categories =
            [
                new("Автоуслуги", "auto-png"),

                new("Услуги строителя", "building-png"),

                new("Красота", "beauty-png"),

                new("Бытовые услуги", "household-png"),

                new("Финансовые услуги", "finance-png"),

                new("Автотовары", "goods-png"),

                new("Парфюм", "perfume-png"),
            ];

            foreach (var c in categories)
                await repository.Add(c);
        }
    }
}
