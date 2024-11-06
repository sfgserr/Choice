using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.CreateCategory
{
    public class CreateCategoryCommand : ICommand
    {
        public CreateCategoryCommand(string title, string iconUri)
        {
            Title = title;
            IconUri = iconUri;
        }

        public string Title { get; }
        
        public string IconUri { get; }
    }
}