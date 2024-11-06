using Administration.Domain.Categories.Rules;
using BuildingBlocks.Domain;

namespace Administration.Domain.Categories
{
    public class Category : Entity, IAggregateRoot
    {
        private Category(string title, string iconUri)
        {
            CheckRule(new CategoryDataMustBeProvidedRule(title, iconUri));
            
            Title = title;
            IconUri = iconUri;
        }

        public static Category Create(string title, string iconUri)
        {
            return new Category(title, iconUri);
        }
        
        public CategoryId Id { get; }
        
        public string Title { get; private set; }

        public string IconUri { get; private set; }

        public void Edit(string title, string iconUri)
        {
            CheckRule(new BasedCategoryCannotBeEditedRule(Id));
            CheckRule(new CategoryDataMustBeProvidedRule(title, iconUri));
            
            Title = title;
            IconUri = iconUri;
        }
    }
}