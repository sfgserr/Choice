using Administration.Application.Contracts;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;

namespace Administration.Application.Commands.EditCategory
{
    internal class EditCategoryCommandHandler : ICommandHandler<EditCategoryCommand>
    {
        private readonly IAdministrationDbContext _dbContext;

        internal EditCategoryCommandHandler(IAdministrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(EditCategoryCommand command)
        {
            var category = await _dbContext.Categories.Get(c => c.Id.Value == command.CategoryId);
            
            category.Edit(command.Title, command.IconUri);
        }
    }
}