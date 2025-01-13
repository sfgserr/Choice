using BuildingBlocks.Domain;
using Users.Domain.Categories;

namespace Users.Domain.Users.Companies.Rules
{
    internal class CategoriesCannotBeEmptyRule : IBusinessRule
    {
        private readonly List<int> _categories;

        internal CategoriesCannotBeEmptyRule(List<int> categories)
        {
            _categories = categories;
        }

        public bool IsBroken => _categories.Count == 0;

        public string Message => "Выберите хотя бы одну категорию";
    }
}
