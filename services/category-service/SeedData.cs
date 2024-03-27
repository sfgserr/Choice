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

            List<Category> categories = new()
            {
                new("Автоуслуги", "auto"),

                new("Услуги строителя", "building"),

                new("Красота", "beauty"),

                new("Бытовые услуги", "household"),

                new("Финансовые услуги", "finance"),

                new("Автотовары", "goods"),

                new("Парфюм", "perfume"),
            };

            foreach (var c in categories)
                await repository.Add(c);
        }
    }
}
