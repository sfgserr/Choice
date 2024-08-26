using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _decorated;
        private readonly UsersContext _usersContext;

        internal UnitOfWorkCommandHandlerDecorator(
            IUnitOfWork unitOfWork, 
            ICommandHandler<T> decorated, 
            UsersContext usersContext)
        {
            _unitOfWork = unitOfWork;
            _decorated = decorated;
            _usersContext = usersContext;
        }

        public async Task Execute(T command)
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                await _decorated.Execute(command);
                await ProcessInternalCommand(command);
                return;
            }

            var transaction = await _unitOfWork.BeginTransactionAsync();

            await _decorated.Execute(command);

            await ProcessInternalCommand(command);

            await _unitOfWork.SaveChangesAsync(transaction);
        }

        private async Task ProcessInternalCommand(T command)
        {
            if (command is InternalCommandBase internalCommandBase)
            {
                var internalCommand = await _usersContext.InternalCommands
                    .FirstOrDefaultAsync(i => i.Id == internalCommandBase.Id);

                if (internalCommand != null)
                    internalCommand.ProcessedDate = DateTime.Now;
            }
        }
    }
}
