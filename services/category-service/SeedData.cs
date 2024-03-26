using Choice.CategoryService.Api.Entities;
using Choice.CategoryService.Api.Repositories;

namespace Choice.CategoryService.Api
{
    public static class SeedData
    {
        public static async Task Seed(IServiceProvider services)
        {
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

            ICategoryRepository repository = services.GetRequiredService<ICategoryRepository>();

            foreach (var c in categories)
                await repository.Add(c);
        }
    }
}
