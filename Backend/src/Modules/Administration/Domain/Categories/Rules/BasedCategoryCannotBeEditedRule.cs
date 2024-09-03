using BuildingBlocks.Domain;

namespace Administration.Domain.Categories.Rules
{
    internal class BasedCategoryCannotBeEditedRule : IBusinessRule
    {
        private readonly CategoryId _categoryId;

        internal BasedCategoryCannotBeEditedRule(CategoryId categoryId)
        {
            _categoryId = categoryId;
        }

        public bool IsBroken => _categoryId.Value < 8;

        public string Message { get; } = "Category is readonly";
    }
}