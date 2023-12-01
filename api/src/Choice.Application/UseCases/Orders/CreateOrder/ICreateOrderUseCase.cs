using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task Execute(List<Category> categories, string description, bool toKnowPrice, bool toKnowAppointmentTime, bool toKnowDeadLine, List<string> photoUris, int searchingRadius);

        void SetOutputPort(IOutputPort outputPort);
    }
}
