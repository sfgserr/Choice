using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Chat.Infrastructure.Data;

namespace Chat.Infrastructure.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _decorated;
        private readonly ChatContext _chatContext;

        public UnitOfWorkCommandHandlerDecorator(
            IUnitOfWork unitOfWork, 
            ICommandHandler<T> decorated, 
            ChatContext chatContext)
        {
            _unitOfWork = unitOfWork;
            _decorated = decorated;
            _chatContext = chatContext;
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
                var internalCommand = await _chatContext.InternalCommands
                    .FirstOrDefaultAsync(i => i.Id == internalCommandBase.Id);

                if (internalCommand != null)
                    internalCommand.Processed = DateTime.UtcNow;
            }
        }
    }
}