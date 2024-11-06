using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.EditCategory
{
    public class EditCategoryCommand : ICommand
    {
        public EditCategoryCommand(int categoryId, string title, string iconUri)
        {
            CategoryId = categoryId;
            Title = title;
            IconUri = iconUri;
        }

        public int CategoryId { get; }

        public string Title { get; }
        
        public string IconUri { get; }
    }
}