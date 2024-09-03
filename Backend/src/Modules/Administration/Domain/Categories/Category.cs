using Administration.Domain.Categories.Rules;
using BuildingBlocks.Domain;

namespace Administration.Domain.Categories
{
    public class Category : Entity, IAggregateRoot
    {
        private Category(CategoryId id, string title, string iconUri)
        {
            CheckRule(new BasedCategoryCannotBeEditedRule(id));
            
            Id = id;
            Title = title;
            IconUri = iconUri;
        }

        public static Category Create(CategoryId id, string title, string iconUri)
        {
            return new Category(id, title, iconUri);
        }
        
        public CategoryId Id { get; }
        
        public string Title { get; private set; }

        public string IconUri { get; private set; }
    }
}