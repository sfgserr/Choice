using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Data;

namespace Payments.Infrastructure.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : 
        ICommandHandlerWithResult<T, TResult> where T : ICommandWithResult<TResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandlerWithResult<T, TResult> _decorated;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            IUnitOfWork unitOfWork, 
            ICommandHandlerWithResult<T, TResult> decorated)
        {
            _unitOfWork = unitOfWork;
            _decorated = decorated;
        }

        public async Task<TResult> Execute(T command)
        {
            if (_unitOfWork.HasActiveTransaction)
                return await _decorated.Execute(command);

            var transaction = await _unitOfWork.BeginTransactionAsync();

            var result = await _decorated.Execute(command);

            await _unitOfWork.SaveChangesAsync(transaction);

            return result;
        }
    }
}