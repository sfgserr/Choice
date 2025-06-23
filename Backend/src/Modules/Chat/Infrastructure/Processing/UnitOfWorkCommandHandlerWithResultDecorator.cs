using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.Data;
using Chat.Application.RealTimeMessaging;
using Chat.Infrastructure.Firebase;

namespace Chat.Infrastructure.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : 
        ICommandHandlerWithResult<T, TResult> where T : ICommandWithResult<TResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FirebaseNotificationService _notificationService;
        private readonly ICommandHandlerWithResult<T, TResult> _decorated;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            IUnitOfWork unitOfWork, 
            FirebaseNotificationService notificationService,
            ICommandHandlerWithResult<T, TResult> decorated)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _decorated = decorated;
        }

        public async Task<TResult> Execute(T command)
        {
            if (_unitOfWork.HasActiveTransaction)
                return await _decorated.Execute(command);

            var transaction = await _unitOfWork.BeginTransactionAsync();

            var result = await _decorated.Execute(command);

            await _unitOfWork.SaveChangesAsync(transaction);
            
            if (_decorated is IRealTimeMessenger messenger)
            {
                await messenger.Send();
            }
            
            if (_decorated is INotifiableCommandHandler<T> commandHandler)
            {
                var notification = commandHandler.GetNotification(command);
                
                await _notificationService.Notify(notification);
            }
            
            return result;
        }
    }
}