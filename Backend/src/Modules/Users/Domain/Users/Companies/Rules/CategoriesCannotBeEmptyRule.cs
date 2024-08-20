using BuildingBlocks.Domain;
using Users.Domain.Categories;

namespace Users.Domain.Users.Companies.Rules
{
    internal class CategoriesCannotBeEmptyRule : IBusinessRule
    {
        private readonly List<CategoryId> _categories;

        internal CategoriesCannotBeEmptyRule(List<CategoryId> categories)
        {
            _categories = categories;
        }

        public bool IsBroken => _categories.Count == 0;

        public string Message => "Categories cannot be empty";
    }
}
