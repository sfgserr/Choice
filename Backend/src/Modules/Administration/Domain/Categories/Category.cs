using Administration.Domain.Categories.Rules;
using BuildingBlocks.Domain;

namespace Administration.Domain.Categories
{
    public class Category : Entity, IAggregateRoot
    {
        private string _title;

        private string _iconUri;

        private Category()
        {
            
        }
        
        private Category(string title, string iconUri)
        {
            CheckRule(new CategoryDataMustBeProvidedRule(title, iconUri));
            
            _title = title;
            _iconUri = iconUri;
        }

        public static Category Create(string title, string iconUri)
        {
            return new Category(title, iconUri);
        }
        
        public CategoryId Id { get; }

        public void Edit(string title, string iconUri)
        {
            CheckRule(new BasedCategoryCannotBeEditedRule(Id));
            CheckRule(new CategoryDataMustBeProvidedRule(title, iconUri));
            
            _title = title;
            _iconUri = iconUri;
        }
    }
}