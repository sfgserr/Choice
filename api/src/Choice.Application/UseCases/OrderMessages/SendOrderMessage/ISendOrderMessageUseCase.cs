using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.SendOrderMessage
{
    public interface ISendOrderMessageUseCase
    {
        Task Execute(User sender, User receiver, double price, DateTime appointmentTime, int duration, Order order);

        void SetOutputPort(IOutputPort outputPort);
    }
}
