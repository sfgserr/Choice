using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Data;

namespace Administration.Infrastructure.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _decorated;

        public UnitOfWorkCommandHandlerDecorator(
            IUnitOfWork unitOfWork, 
            ICommandHandler<T> decorated)
        {
            _unitOfWork = unitOfWork;
            _decorated = decorated;
        }

        public async Task Execute(T command)
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                await _decorated.Execute(command);
                return;
            }

            var transaction = await _unitOfWork.BeginTransactionAsync();

            await _decorated.Execute(command);

            await _unitOfWork.SaveChangesAsync(transaction);
        }
    }
}