using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Payments.Infrastructure.Data;

namespace Payments.Infrastructure.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _decorated;
        private readonly PaymentsContext _paymentsContext;

        public UnitOfWorkCommandHandlerDecorator(
            IUnitOfWork unitOfWork,
            ICommandHandler<T> decorated,
            PaymentsContext paymentsContext)
        {
            _unitOfWork = unitOfWork;
            _decorated = decorated;
            _paymentsContext = paymentsContext;
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
                var internalCommand = await _paymentsContext.InternalCommands
                    .FirstOrDefaultAsync(i => i.Id == internalCommandBase.Id);

                if (internalCommand != null)
                    internalCommand.Processed = DateTime.UtcNow;
            }
        }
    }
}
