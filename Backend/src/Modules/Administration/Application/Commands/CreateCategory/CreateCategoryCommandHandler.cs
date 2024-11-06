using Administration.Application.Contracts;
using Administration.Domain.Categories;
using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.CreateCategory
{
    internal class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly IAdministrationDbContext _dbContext;

        internal CreateCategoryCommandHandler(IAdministrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(CreateCategoryCommand command)
        {
            var category = Category.Create(
                command.Title,
                command.IconUri);

            await _dbContext.Categories.AddAsync(category);
        }
    }
}