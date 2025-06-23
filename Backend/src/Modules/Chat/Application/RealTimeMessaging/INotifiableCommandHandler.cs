namespace Chat.Application.RealTimeMessaging
{
    public interface INotifiableCommandHandler<TCommand>
    {
        Notification GetNotification(TCommand command);
    }
}